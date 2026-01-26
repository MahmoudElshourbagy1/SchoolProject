using SchoolProject.Core.Features.Students.Queries.Resuilts;
using SchoolProject.Data._ُEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudnetByIdMapping()
        {
            CreateMap<Student, GetSingleStudentRes>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Departments.DName));
        }
    }
}
