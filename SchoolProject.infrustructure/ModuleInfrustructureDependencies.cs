using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Abstracts.Procedures;
using SchoolProject.infrustructure.Abstracts.Views;
using SchoolProject.infrustructure.infrustructureBase;
using SchoolProject.infrustructure.Repositories;
using SchoolProject.infrustructure.Repositories.Procedures;
using SchoolProject.infrustructure.Repositories.Views;

namespace SchoolProject.infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            // Here you can add your infrastructure dependencies
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IInstructorsRepository, InstructorsRepository>();
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();
            services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
