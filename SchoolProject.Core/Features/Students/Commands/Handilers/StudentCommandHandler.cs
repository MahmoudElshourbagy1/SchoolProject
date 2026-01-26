using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data._ُEntities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Commands.Handilers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between req and student
            var studentmapper = _mapper.Map<Student>(request);
            //Add 
            var result =await _studentService.AddAsync(studentmapper);
            // Check Condition
            if(result== "Exist")
            {
                return UnprocessableEntity<string>("Name is Exist");
            }
            //return res
            else if (result== "Success")
            {
                return Created("Added Success");
            }
            else
            {
                return BadRequest<string>();
            }
           
        }
    }
}
