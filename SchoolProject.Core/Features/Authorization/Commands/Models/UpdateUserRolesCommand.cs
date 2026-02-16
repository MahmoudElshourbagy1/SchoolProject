using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.DTOS;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest,
                                           IRequest<Response<string>>
    {

    }
}
