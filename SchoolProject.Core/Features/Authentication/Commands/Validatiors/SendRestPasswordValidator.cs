using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validatiors
{
    public class SendRestPasswordValidator : AbstractValidator<SendRestPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public SendRestPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);

        }
        public void ApplyCustomValidationRules()
        {


        }
    }
}
