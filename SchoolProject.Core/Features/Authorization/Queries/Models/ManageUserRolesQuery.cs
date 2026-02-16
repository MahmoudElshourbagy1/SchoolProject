using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.DTOS;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
