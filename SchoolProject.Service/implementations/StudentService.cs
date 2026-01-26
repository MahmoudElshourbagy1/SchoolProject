using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Service.implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository= studentRepository;
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
            //Check if the name is Exist or Not
            var studentResult=_studentRepository.GetTableNoTracking().Where(x=>x.Name.Equals(student.Name)).FirstOrDefault();
            if (studentResult != null) 
            {
                return "Exist";
            }
            //Added Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }
    }
}
