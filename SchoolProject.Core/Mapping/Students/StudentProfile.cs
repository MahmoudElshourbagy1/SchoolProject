using AutoMapper;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudnetListMapping();
            GetStudnetByIdMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
            GetStudnetPaginationMapping();
        }
    }
}
