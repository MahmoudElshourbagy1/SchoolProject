using Microsoft.Extensions.DependencyInjection;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Repositories;

namespace SchoolProject.infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            // Here you can add your infrastructure dependencies
            services.AddTransient<IStudentRepository, StudentRepository>();
          
            return services;
        }
    }
}
