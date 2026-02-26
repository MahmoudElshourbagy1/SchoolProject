using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthServices.Interfaces;

namespace SchoolProject.Service.AuthServices.implementations
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public CurrentUserServices(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task<User> GetcurrentUserAsync()
        {
            var userId = GetuserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }

        public int GetuserId()
        {
            var userId = _contextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimModel.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetcurrentUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
