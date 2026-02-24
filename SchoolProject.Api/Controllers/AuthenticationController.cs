using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> CreateUser([FromForm] SignInCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpGet(Router.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var res = await Mediator.Send(query);
            return NewResult(res);
        }
        [HttpGet(Router.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var res = await Mediator.Send(query);
            return NewResult(res);
        }
        [HttpPost(Router.Authentication.SendRestPassword)]
        public async Task<IActionResult> SendRestPassword([FromQuery] SendRestPasswordCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpGet(Router.Authentication.ConfirmRestPassword)]
        public async Task<IActionResult> ConfirmRestPassword([FromQuery] RestPasswordQuery query)
        {
            var res = await Mediator.Send(query);
            return NewResult(res);
        }
        [HttpPost(Router.Authentication.RestPassword)]
        public async Task<IActionResult> RestPassword([FromForm] RestPassowdCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
    }
}
