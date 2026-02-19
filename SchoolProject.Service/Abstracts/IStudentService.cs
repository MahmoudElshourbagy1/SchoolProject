using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Enums;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        //With INclouds
        public Task<Student> GetStudentsByIDAsync(int id);
        public Task<Student> GetByIDAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameArExist(string nameAr);
        public Task<bool> IsNameEnExist(string nameEn);
        public Task<bool> IsNameArExistExcludeSelf(string nameAr, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetStudentQuerable();
        public IQueryable<Student> GetStudentByDepartmentIdQuerable(int DIO);

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentEnum studentEnum, string search);
    }
}