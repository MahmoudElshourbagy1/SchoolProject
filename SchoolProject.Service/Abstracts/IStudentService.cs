using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        //With INclouds
        public Task<Student> GetStudentsByIDAsync(int id);
        public Task<Student> GetByIDAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameExistAsync(string name);
        public Task<bool> IsNameExisExcludeSelfAsync(string name, int id);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetStudentQuerable();
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentEnum studentEnum, string search);
    }
}