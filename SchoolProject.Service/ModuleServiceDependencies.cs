using Microsoft.Extensions.DependencyInjection;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Repositories;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.implementations;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            // Here you can add your infrastructure dependencies
            services.AddTransient<IStudentService, StudentService>();
            
            return services;
        }

    }
}
