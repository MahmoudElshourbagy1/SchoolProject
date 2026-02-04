using SchoolProject.Data.Entities;
using SchoolProject.infrustructure.Abstracts.Procedures;
using SchoolProject.infrustructure.Data;
using StoredProcedureEFCore;

namespace SchoolProject.infrustructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        private readonly AppBDContext _dbContext;

        public DepartmentStudentCountProcRepository(AppBDContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProc>();
            await _dbContext.LoadStoredProc(nameof(DepartmentStudentCountProc))
                   .AddParam(nameof(DepartmentStudentCountProcParameters.DIO), parameters.DIO)
                   .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());
            return rows;
        }
    }
}