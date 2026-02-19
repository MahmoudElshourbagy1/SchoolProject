using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler,
                          IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService, UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            var result = await _authorizationService.ManageUserClaimsData(user);
            return Success(result);
        }
    }
}
