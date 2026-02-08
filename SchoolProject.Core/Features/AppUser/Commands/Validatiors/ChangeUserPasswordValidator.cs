using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.AppUser.Commands.Validatiors
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);

            RuleFor(x => x.ConfirmPassword)
           .Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKeys.PassNotEqualConfrimPass]);

        }
        public void ApplyCustomValidationRules()
        {


        }

    }
}
