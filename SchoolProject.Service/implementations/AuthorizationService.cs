
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOS;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrustructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppBDContext _appBDContext;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppBDContext appBDContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appBDContext = appBDContext;
        }

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return "Role added successfully";
            }
            else
            {
                return "Failed to add role";
            }
        }
        public async Task<bool> IsRoleExistByName(string roleName)
        {

            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            // check if role exist or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) return "Role not found";
            role.Name = request.Name;
            // if exist get the role and Edit it
            var result = await _roleManager.UpdateAsync(role);
            // success return message
            if (result.Succeeded) return "Role updated successfully";
            var errors = string.Join("_", result.Errors);
            return errors;
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //check if user has this rol or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return ex
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            var errors = string.Join("_", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            else return true;
        }

        public async Task<List<Role>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role> GetRolesById(int id)
        {
            var roles = await _roleManager.FindByIdAsync(id.ToString());
            return roles;
        }

        public async Task<ManageUserRolesResult> GetManageUserRolesData(User user)
        {
            var respone = new ManageUserRolesResult();
            var Roles = new List<Roles>();
            //userRole
            var userRoles = await _userManager.GetRolesAsync(user);
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            respone.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new Roles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                Roles.Add(userRole);
            }
            respone.Roles = Roles;
            return respone;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest result)
        {
            var transact = await _appBDContext.Database.BeginTransactionAsync();
            try
            {
                //get user old roles 
                var user = await _userManager.FindByIdAsync(result.UserId.ToString());

                if (user == null)
                {
                    return "UserIsNull";
                }

                var userrole = await _userManager.GetRolesAsync(user);
                //delete roles
                var removeresult = await _userManager.RemoveFromRolesAsync(user, userrole);
                if (!removeresult.Succeeded) return "FailedToRemoveOldRoles";
                var selectedRoles = result.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
                //add the roles hasrole = true
                var addrolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addrolesresult.Succeeded) return "FailedToAddNewRoles";
                await transact.CommitAsync();
                return "Success";

            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
    }
}
