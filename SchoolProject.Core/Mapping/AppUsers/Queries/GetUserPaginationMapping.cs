using SchoolProject.Core.Features.AppUser.Queries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.AppUsers
{
    public partial class AppUsersProfile
    {
        public void GetUserPagination()
        {

            CreateMap<User, GetUserPaginationResponse>();
        }
    }
}
