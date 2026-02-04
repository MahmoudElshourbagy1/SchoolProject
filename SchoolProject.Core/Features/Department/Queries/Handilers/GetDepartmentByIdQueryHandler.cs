using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data._ُEntities;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Department.Queries.Handilers
{
    public class GetDepartmentByIdQueryHandler :
        ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
         IRequestHandler<GetDepartmentStudentListCountQuery, Response<List<GetDepartmentStudentListCountResults>>>,
         IRequestHandler<GetDepartmentStudentCountByIDQuery, Response<GetDepartmentStudentCountByIDResult>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public GetDepartmentByIdQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IDepartmentService departmentService, IMapper mapper, IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _studentService = studentService;
            _mapper = mapper;

        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //service GetbyId Include st sub ins
            var response = await _departmentService.GetDepartmentById(request.Id);
            // Check IS not Exsit
            if (response == null)
            {
                return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            //Mapping
            var Mapper = _mapper.Map<GetDepartmentByIdResponse>(response);
            //Pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = _studentService.GetStudentByDepartmentIdQuerable(request.Id);
            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            Mapper.StudentList = PaginatedList;

            //return res
            return Success(Mapper);
        }
        public async Task<Response<List<GetDepartmentStudentListCountResults>>> Handle(GetDepartmentStudentListCountQuery request, CancellationToken cancellationToken)
        {
            var viewDepartmentResult = await _departmentService.GetViewDepartmentDataAsync();
            var result = _mapper.Map<List<GetDepartmentStudentListCountResults>>(viewDepartmentResult);
            return Success(result);

        }

        public async Task<Response<GetDepartmentStudentCountByIDResult>> Handle(GetDepartmentStudentCountByIDQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<DepartmentStudentCountProcParameters>(request);
            var procResult = await _departmentService.GetDepartmentStudentCountProcs(parameters);
            var result = _mapper.Map<GetDepartmentStudentCountByIDResult>(procResult.FirstOrDefault());
            return Success(result);

        }
    }
}
