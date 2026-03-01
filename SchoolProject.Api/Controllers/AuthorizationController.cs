using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpPost(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(res);
        }
        [HttpGet(Router.AuthorizationRouting.RoleList)]
        public async Task<IActionResult> GetRoleList()
        {
            var res = await Mediator.Send(new GetRolesListQuery());
            return NewResult(res);
        }
        [SwaggerOperation(Summary = "idالصلاحية عن طريق ال", OperationId = "RoleById")]
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var res = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(res);
        }
        [SwaggerOperation(Summary = " ادارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int userid)
        {
            var res = await Mediator.Send(new ManageUserRolesQuery() { UserId = userid });
            return NewResult(res);
        }
        [SwaggerOperation(Summary = " تعديل صلاحيات المستخدمين", OperationId = "UpdateUserRoles")]
        [HttpGet(Router.AuthorizationRouting.ManageUserCliams)]
        public async Task<IActionResult> ManageUserCliams([FromRoute] int userid)
        {
            var res = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userid });
            return NewResult(res);
        }
        [SwaggerOperation(Summary = " ادارة صلاحيات الاستخدام المستخدمين", OperationId = "ManageUserClaims")]
        [HttpPut(Router.AuthorizationRouting.UserRolesUpdate)]
        public async Task<IActionResult> UserRolesUpdate([FromBody] UpdateUserRolesCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [SwaggerOperation(Summary = " تعديل صلاحيات  الاستخدام المستخدمين", OperationId = "UpdateUserClaims")]
        [HttpPut(Router.AuthorizationRouting.UodateUserClaims)]
        public async Task<IActionResult> UodateUserClaims([FromBody] UodateUserClaimsCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }

    }
}
