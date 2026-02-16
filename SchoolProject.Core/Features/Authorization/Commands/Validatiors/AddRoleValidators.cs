using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validatiors
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        public AddRoleValidators(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);


        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.RoleName)
                     .MustAsync(async (name, cancellation) => !await _authorizationService.IsRoleExistByName(name))
                     .WithMessage(_localizer[SharedResourcesKeys.IsExist]);

        }
    }
}
