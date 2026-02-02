using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data._ُEntities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
               .ForMember(
                dest => dest.DIO,
                opt => opt.MapFrom(src => src.DepartmementId)
                )
               .ForMember(
                dest => dest.NameEn,
                opt => opt.MapFrom(src => src.NameEn)
                )
            .ForMember(
                dest => dest.NameAr,
                opt => opt.MapFrom(src => src.NameAr)
                )
                 .ForMember(
                dest => dest.StudID,
                opt => opt.MapFrom(src => src.id)
                );
        }
    }
}
