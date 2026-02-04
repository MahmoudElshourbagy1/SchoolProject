using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Repositories
{
    public class InstructorsRepository : GenericRepositoryAsync<Instructor>, IInstructorsRepository
    {
        private readonly DbSet<Instructor> _instructor;
        public InstructorsRepository(AppBDContext dbContext) : base(dbContext)
        {
            _instructor = dbContext.Set<Instructor>();
        }
    }
}
