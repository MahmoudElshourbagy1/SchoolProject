using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Helpers;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }



        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentsByIDAsync(int id)
        {
            // var student =await _studentRepository.GetByIdAsync(id);
            var student = _studentRepository.GetTableNoTracking().Include(x => x.Departments)
                 .Where(x => x.StudID == id).FirstOrDefault();
            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            //Added Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            return await _studentRepository
                .GetTableNoTracking()
                .AnyAsync(x => x.Name == name);
        }



        public async Task<bool> IsNameExisExcludeSelfAsync(string name, int id)
        {
            return await _studentRepository
                .GetTableNoTracking()
                .AnyAsync(x => x.Name == name && x.StudID != id);
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);

            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ex.Message;
            }


        }

        public async Task<Student> GetByIDAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;

        }

        public IQueryable<Student> GetStudentQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Departments).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentEnum studentEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Departments).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Name.Contains(search) || x.Address.Contains(search) || x.Departments.DName.Contains(search));
            }
            switch (studentEnum)
            {
                case StudentEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
                case StudentEnum.Name:
                    querable = querable.OrderBy(x => x.Name);
                    break;
                case StudentEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Departments.DName);
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
            }
            return querable;
        }
    }
}
