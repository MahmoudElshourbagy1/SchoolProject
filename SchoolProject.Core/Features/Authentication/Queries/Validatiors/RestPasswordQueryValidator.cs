using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validatiors
{
    public class RestPasswordQueryValidator : AbstractValidator<RestPasswordQuery>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public RestPasswordQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.RequiredField]);

        }
        public void ApplyCustomValidationRules()
        {


        }
    }
}
