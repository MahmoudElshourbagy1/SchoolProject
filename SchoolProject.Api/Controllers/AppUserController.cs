

using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Core.Features.AppUser.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AppUserController : AppControllerBase
    {
        [HttpPost(Router.AppUserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpGet(Router.AppUserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var res = await Mediator.Send(query);
            return Ok(res);
        }
        [HttpGet(Router.AppUserRouting.GetByID)]
        public async Task<IActionResult> GetstudentById([FromRoute] int id)
        {
            var res = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(res);
        }

        [HttpPut(Router.AppUserRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditUserCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
    }
}
