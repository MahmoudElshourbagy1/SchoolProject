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
    public class StudentQueryHandler :ResponseHandler, IRequestHandler<GetStudentListQuery,Response< List<GetStudentListRes>>>
        , IRequestHandler<GetStudentByIdQuery,Response<GetSingleStudentRes>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
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

        public async Task<Response<GetSingleStudentRes>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsByIDAsync(request.Id);
            if (student == null) 
            {
                return NotFound<GetSingleStudentRes>("student not found");
            }
            var result = _mapper.Map<GetSingleStudentRes>(student);
            return Success(result);
        }
    }
}
