using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.AuthServices.Interfaces
{
    public interface ICurrentUserServices
    {
        public Task<User> GetcurrentUserAsync();
        public int GetuserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
