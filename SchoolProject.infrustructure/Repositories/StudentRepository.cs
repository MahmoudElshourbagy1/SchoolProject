using Microsoft.EntityFrameworkCore;
using SchoolProject.Data._ُEntities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.infrustructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {

        #region Fields
        private readonly DbSet<Student> _students;
        #endregion
        #region Constructor
        public StudentRepository(AppBDContext dbContext) :base(dbContext)
        {
            _students= dbContext.Set<Student>();
        }
        #endregion
        #region Handles functions

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _students.Include(x=>x.Departments).ToListAsync();
        }
        #endregion
    }
}
