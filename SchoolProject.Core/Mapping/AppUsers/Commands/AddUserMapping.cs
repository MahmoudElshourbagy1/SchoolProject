using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.AppUsers
{
    public partial class AppUsersProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
