using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJWTToken(string token);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken accesstoken, string token, string refreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken accesstoken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string token);
    }
}
