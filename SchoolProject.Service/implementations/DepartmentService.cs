using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Abstracts.Procedures;
using SchoolProject.infrustructure.Abstracts.Views;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        private readonly IDepartmentStudentCountProcRepository _departmentStudentCountProcRepository;
        public DepartmentService(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewDepartmentRepository, IDepartmentStudentCountProcRepository departmentStudentCountProcRepository)
        {
            _departmentRepository = departmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
            _departmentStudentCountProcRepository = departmentStudentCountProcRepository;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var student = await _departmentRepository.GetTableNoTracking().Where(x => x.DIO.Equals(id))
                   .Include(x => x.DepartmentsSubjects).ThenInclude(x => x.Subjects)
                   .Include(x => x.Instructors)
                   .Include(x => x.Instructor)
                   .FirstOrDefaultAsync();
            return student;
        }

        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            return await _departmentStudentCountProcRepository.GetDepartmentStudentCountProcs(parameters);
        }

        public async Task<List<ViewDepartment>> GetViewDepartmentDataAsync()
        {
            var viewDepartment = await _viewDepartmentRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<bool> IsDepartmentIdExist(int departmentId)
        {
            return await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DIO.Equals(departmentId));
        }
    }
}
