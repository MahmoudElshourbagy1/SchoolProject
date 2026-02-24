using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Queries.Validatiors
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);
            RuleFor(x => x.code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);
        }
        public void ApplyCustomValidationRules()
        {


        }
    }
}
