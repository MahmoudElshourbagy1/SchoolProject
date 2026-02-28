using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.infrustructure.Abstracts.Procedures
{
    public interface IDepartmentStudentCountProcRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters);
    }
}
