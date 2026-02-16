using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var role = await _roleManager.Roles.CountAsync();
            if (role <= 0)
            {
                await _roleManager.CreateAsync(new Role { Name = "Admin" });
                await _roleManager.CreateAsync(new Role { Name = "User" });
            }
        }
    }
}
