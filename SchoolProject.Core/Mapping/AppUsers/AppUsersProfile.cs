using AutoMapper;

namespace SchoolProject.Core.Mapping.AppUsers
{
    public partial class AppUsersProfile : Profile
    {
        public AppUsersProfile()
        {
            AddUserMapping();
            GetUserPagination();
            GetUserByIdMapping();
        }
    }
}
