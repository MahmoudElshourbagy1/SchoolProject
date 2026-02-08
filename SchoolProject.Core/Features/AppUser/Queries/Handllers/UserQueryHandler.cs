using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.AppUser.Queries.Models;
using SchoolProject.Core.Features.AppUser.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.AppUser.Queries.Handllers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
        , IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly UserManager<User> _userManager;
        public UserQueryHandler(IMapper mapper, IStringLocalizer<SharedResources> stringlocalizer, UserManager<User> userManager) : base(stringlocalizer)
        {
            _mapper = mapper;
            _stringlocalizer = stringlocalizer;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginationResponse>(users).
                                                    ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return NotFound<GetUserByIdResponse>(_stringlocalizer[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
    }
}
