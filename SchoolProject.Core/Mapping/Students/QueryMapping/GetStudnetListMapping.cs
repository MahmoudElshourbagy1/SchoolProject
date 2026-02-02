using SchoolProject.Core.Features.Students.Queries.Resuilts;
using SchoolProject.Data._ُEntities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudnetListMapping()
        {
            CreateMap<Student, GetStudentListRes>()
              .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Departments.Localize(src.Departments.DNameAr, src.Departments.DNameEn)))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
