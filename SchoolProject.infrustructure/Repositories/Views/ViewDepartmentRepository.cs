using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrustructure.Abstracts.Views;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        private DbSet<ViewDepartment> viewDepartment;

        public ViewDepartmentRepository(AppBDContext dbContext) : base(dbContext)
        {
            viewDepartment = dbContext.Set<ViewDepartment>();
        }
    }
}
