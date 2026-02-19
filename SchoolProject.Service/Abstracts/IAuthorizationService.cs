using SchoolProject.Data.DTOS;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistById(int roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRolesById(int id);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest result);
        public Task<ManageUserClaimsResult> ManageUserClaimsData(User user);
        public Task<string> UpdateUserClaims(UodateUserClaimsRequest result);

    }
}
