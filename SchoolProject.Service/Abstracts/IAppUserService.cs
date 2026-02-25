using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IAppUserService
    {
        public Task<string> AddUserAsync(User user, string Password);
    }
}
