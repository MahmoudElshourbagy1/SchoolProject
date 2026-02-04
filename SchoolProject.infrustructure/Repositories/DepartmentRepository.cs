using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;
        public DepartmentRepository(AppBDContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }
    }
}
