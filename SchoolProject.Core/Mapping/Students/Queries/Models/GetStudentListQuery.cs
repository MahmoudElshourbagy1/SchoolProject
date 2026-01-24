using MediatR;
using SchoolProject.Data._ُEntities;

namespace SchoolProject.Core.Mapping.Students.Queries.Models
{
    public class GetStudentListQuery :IRequest<List<Student>>
    {
    }
}
