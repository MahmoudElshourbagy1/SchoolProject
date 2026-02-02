using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {
        public int id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string Address { get; set; }

        public int? Phone { get; set; }
        public int DepartmementId { get; set; }
    }
}
