using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Resuilts;
using SchoolProject.Data._ُEntities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler :ResponseHandler, IRequestHandler<GetStudentListQuery,Response< List<GetStudentListRes>>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetStudentListRes>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
        var studentList= await _studentService.GetStudentsListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListRes>>(studentList);
            return Success(studentListMapper);
        }
    }
}
