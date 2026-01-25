using SchoolProject.Data._ُEntities;
using SchoolProject.infrustructure.infrustructureBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.infrustructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync <Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
    }
}
