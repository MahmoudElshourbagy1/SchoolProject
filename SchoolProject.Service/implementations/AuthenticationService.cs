using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.implementations
{


    public class AuthenticationService : IAuthenticationService
    {




        public async Task<string> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateLifetime = false
            };
            try
            {
                var validator = tokenHandler.ValidateToken(token, parameters, out var validatedToken);

                if (validatedToken == null)
                {
                    return "Invalid token";
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public JwtSecurityToken ReadJWTToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = GetClaims(user, roles.ToList());
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return (token, tokenString);
        }
        //GetClaims
        public List<Claim> GetClaims(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.UserName??""),
                new Claim (ClaimTypes.Email, user.Email??""),
                new Claim (ClaimTypes.MobilePhone, user.PhoneNumber??""),
                new Claim (ClaimTypes.Name, user.FullName),
                new Claim (nameof(UserClaimModel.Id), user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        //GenerateRefreshToken
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //GetRefreshToken
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                UserName = username,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                TokenString = GenerateRefreshToken()
            };
            _refreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }


        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _userManager = userManager;
        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (token, tokenString) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = tokenString,
                RefreshToken = refreshToken.TokenString,
                JwtId = token.Id,
                IsUsed = true,
                IsRevoked = false,
                AddTime = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate)
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var res = new JwtAuthResult();
            res.AccessToken = tokenString;
            res.refreshToken = refreshToken;
            return res;
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken accesstoken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var res = new JwtAuthResult();
            res.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = accesstoken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.UserName))?.Value;
            refreshTokenResult.ExpiryDate = (DateTime)expiryDate;
            refreshTokenResult.TokenString = refreshToken;
            res.refreshToken = refreshTokenResult;
            return res;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken accesstoken, string token, string refreshToken)
        {
            if (accesstoken == null || !accesstoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("Invalid token", null);
            }
            if (accesstoken.ValidTo > DateTime.UtcNow)
            {
                return ("Token is Not Expired", null);
            }
            //Get user 
            var userId = accesstoken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.Id))?.Value;
            var username = await _refreshTokenRepository.GetTableNoTracking().
                FirstOrDefaultAsync(r => r.Token == token && r.RefreshToken == refreshToken && r.UserId == int.Parse(userId));

            //Validate the refresh token
            if (username == null)
            {
                return ("RefreshToken is not Found", null);
            }

            if (username.ExpiryDate < DateTime.UtcNow)
            {
                username.IsRevoked = true;
                username.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(username);
                return ("Refresh Token is Expired", null);
            }
            var expiredDate = username.ExpiryDate;
            return (userId, expiredDate);
        }
    }
}
