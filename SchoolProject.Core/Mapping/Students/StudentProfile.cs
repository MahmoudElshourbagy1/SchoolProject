using AutoMapper;
using SchoolProject.Core.Features.Students.Queries.Resuilts;
using SchoolProject.Data._ُEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudnetListMapping();
        }
    }
}
