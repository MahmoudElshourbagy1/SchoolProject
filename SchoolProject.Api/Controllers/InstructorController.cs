using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instuctors.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpGet(Router.InstructorRouting.GetSalarySummation)]
        public async Task<IActionResult> GetSalarySummation()
        {
            var res = await Mediator.Send(new GetSummationSalaryOfInstructorQuery());
            return Ok(res);
        }
    }
}
