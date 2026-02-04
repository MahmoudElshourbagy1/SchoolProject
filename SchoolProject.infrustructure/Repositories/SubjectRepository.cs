using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        private readonly DbSet<Subjects> _subjects;

        public SubjectRepository(AppBDContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subjects>();

        }
    }
}
