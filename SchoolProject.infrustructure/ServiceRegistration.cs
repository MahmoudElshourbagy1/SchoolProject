using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrustructure.Data;

namespace SchoolProject.infrustructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(op =>
            {
                // Password settings.
                op.Password.RequireDigit = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireUppercase = true;
                op.Password.RequiredLength = 6;
                op.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                op.Lockout.MaxFailedAccessAttempts = 5;
                op.Lockout.AllowedForNewUsers = true;

                // User settings.
                op.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                op.User.RequireUniqueEmail = false;
                op.SignIn.RequireConfirmedEmail = false;

            }).AddEntityFrameworkStores<AppBDContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
