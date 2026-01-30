using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;


namespace SchoolProject.Api.Controllers
{

    [ApiController]
    public class StudentController : AppControllerBase
    {


        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentsList()
        {
            var res = await Mediator.Send(new GetStudentListQuery());
            return NewResult(res);
        }
        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var res = await Mediator.Send(query);
            return Ok(res);
        }
        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetstudentById([FromRoute] int id)
        {
            var res = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(res);
        }
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {
            var res = await Mediator.Send(command);
            return NewResult(res);
        }
        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return NewResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
    }
}
