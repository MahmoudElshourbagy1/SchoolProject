using SchoolProject.Core.Features.Instuctors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instuctors
{
    public partial class InstuctorProfile
    {
        public void AddInstrructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
              .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}