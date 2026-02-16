using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handllers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>

    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;

        public RoleCommandHandler(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService) : base(localizer)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Role added successfully")
            {
                return Success("");
            }
            else
            {
                return BadRequest<string>(_localizer[SharedResourcesKeys.AddFaild]);
            }
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "Role not found") return NotFound<string>();
            else if (result == "Role updated successfully") return Success((string)_localizer[SharedResourcesKeys.Updated]);
            else return BadRequest<string>(result);

        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(_localizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success") return Success((string)_localizer[SharedResourcesKeys.Deleted]);
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_localizer[SharedResourcesKeys.Success]);
        }
    }
}
