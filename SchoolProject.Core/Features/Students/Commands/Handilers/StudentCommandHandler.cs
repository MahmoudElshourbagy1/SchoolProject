using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data._ُEntities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handilers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>

    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between req and student
            var studentmapper = _mapper.Map<Student>(request);
            //Add 
            var result = await _studentService.AddAsync(studentmapper);
            // Check Condition
            if (result == "Success")
            {
                return Created("");
            }
            else
            {
                return BadRequest<string>();
            }

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if th id is Exsit
            var student = await _studentService.GetByIDAsync(request.id);
            //return NotFound<string>("student not found");
            if (student == null) return NotFound<string>("student not found");
            //mapping Between req and student
            var studentmapper = _mapper.Map(request, student);
            //call update method from service
            var result = await _studentService.EditAsync(studentmapper);
            //return Success("Updated Success");
            if (result == "Success")
            {
                return Success($"Edit Success {studentmapper.StudID}");
            }
            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if th id is Exsit
            var student = await _studentService.GetByIDAsync(request.Id);
            //return NotFound<string>("student not found");
            if (student == null) return NotFound<string>("student not found");
            //call Delete method from service
            var result = await _studentService.DeleteAsync(student);
            //return Success("Updated Success");
            if (result == "Success")
            {
                return Deleted<string>($"Edit Success {request.Id}");
            }
            else
            {
                return BadRequest<string>();
            }
        }
    }
}
