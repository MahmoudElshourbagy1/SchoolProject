using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.implementations;
using System.Reflection;

namespace SchoolProject.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {

            // Here you can add your infrastructure dependencies
            // configure MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //configure AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
