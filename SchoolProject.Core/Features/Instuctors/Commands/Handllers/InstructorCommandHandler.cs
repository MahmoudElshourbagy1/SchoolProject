using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instuctors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instuctors.Commands.Handllers
{
    public class InstructorCommandHandler : ResponseHandler,
        IRequestHandler<AddInstructorCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        public InstructorCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, IInstructorService instructorService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _instructorService = instructorService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoFileUploaded": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoFileUploaded]);
                case "FailedToUploadFile": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadFile]);
                case "FailedInAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedInAdd]);

            }
            return Success("");
        }
    }
}
