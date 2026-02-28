using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instuctors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instuctors.Queries.Handllers
{
    public class InstuctorQueryHandler : ResponseHandler,
        IRequestHandler<GetSummationSalaryOfInstructorQuery, Response<decimal>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IInstructorService _instructorService;
        public InstuctorQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> localizer, IInstructorService instructorService) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _instructorService = instructorService;
        }

        public async Task<Response<decimal>> Handle(GetSummationSalaryOfInstructorQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetSalaryInstructorOfSummation();
            return Success(result);
        }
    }
}
