using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "schollProject",
                    Country = "Egypt",
                    Address = "Cairo",
                    PhoneNumber = "123456789",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "Mah123@");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
