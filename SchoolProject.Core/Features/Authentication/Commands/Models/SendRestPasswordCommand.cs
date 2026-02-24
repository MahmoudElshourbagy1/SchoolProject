using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class SendRestPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }

    }
}
