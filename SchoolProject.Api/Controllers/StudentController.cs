using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Mapping.Students.Queries.Models;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IMediator _mediator;
        public StudentController(IMediator  mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/GetStudents/List")]
        public async Task<IActionResult> GetStudentsList()
        {
            var res =await _mediator.Send(new GetStudentListQuery());
            return Ok(res);
        }
    }
}
