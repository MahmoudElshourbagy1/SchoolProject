using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.AuthServices.implementations;
using SchoolProject.Service.AuthServices.Interfaces;
using SchoolProject.Service.implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            // Here you can add your infrastructure dependencies
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<ICurrentUserServices, CurrentUserServices>();
            services.AddTransient<IInstructorService, InstructorService>();
            return services;
        }

    }
}
