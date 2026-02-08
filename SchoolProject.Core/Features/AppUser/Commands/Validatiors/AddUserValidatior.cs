using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.AppUser.Commands.Validatiors
{
    public class AddUserValidatior : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AddUserValidatior(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField])
                 .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage(_localizer[SharedResourcesKeys.BadRequest])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);
            RuleFor(x => x.Password)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);
            RuleFor(x => x.ConfirmPassword)
           .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PassNotEqualConfrimPass]);

        }
        public void ApplyCustomValidationRules()
        {


        }
    }
}
