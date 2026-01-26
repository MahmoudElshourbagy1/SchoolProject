using SchoolProject.Data._ُEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentsByIDAsync(int id);
        public Task<string> AddAsync(Student student);
    }
}
