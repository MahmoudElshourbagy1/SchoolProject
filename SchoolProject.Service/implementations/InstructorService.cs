using SchoolProject.infrustructure.Abstracts.Functions;
using SchoolProject.infrustructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class InstructorService : IInstructorService
    {
        private readonly AppBDContext _appBDContext;
        private readonly IInstructorsFunctionsRepository _instructorsFunctionsRepository;
        public InstructorService(AppBDContext appBDContext, IInstructorsFunctionsRepository instructorsFunctionsRepository)
        {
            _appBDContext = appBDContext;
            _instructorsFunctionsRepository = instructorsFunctionsRepository;
        }
        public async Task<decimal> GetSalaryInstructorOfSummation()
        {
            decimal result = 0;
            result = _instructorsFunctionsRepository.GetSalaryInstructorOfSummation("select dbo.GetSalarySummation()");
            return result;
        }
    }
}
