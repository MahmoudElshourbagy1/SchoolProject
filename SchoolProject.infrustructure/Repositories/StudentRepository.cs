using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.infrustructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        #region Fields
        private readonly AppBDContext _context;
        #endregion
        #region Constructor
        public StudentRepository(AppBDContext context)
        {
            _context = context;
        }
        #endregion
        #region Handles functions

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _context.students.ToListAsync();
        }
        #endregion
    }
}
