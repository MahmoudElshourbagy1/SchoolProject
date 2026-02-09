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
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber)
            };
            return claims;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

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
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokens = new ConcurrentDictionary<string, RefreshToken>();

        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var claims = GetClaims(user);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = tokenString,
                RefreshToken = refreshToken.TokenString,
                JwtId = token.Id,
                IsUsed = false,
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

    }
}
