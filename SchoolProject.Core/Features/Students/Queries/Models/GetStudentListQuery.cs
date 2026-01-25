using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Resuilts;
using SchoolProject.Data._ُEntities;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery :IRequest<Response<List<GetStudentListRes>>>
    {
    }
}
