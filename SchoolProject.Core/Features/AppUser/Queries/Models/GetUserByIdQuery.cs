using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.AppUser.Queries.Results;

namespace SchoolProject.Core.Features.AppUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
