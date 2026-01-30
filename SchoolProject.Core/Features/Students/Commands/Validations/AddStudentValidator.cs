using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;


namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");
            RuleFor(x => x.DepartmementId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
     .MustAsync(async (name, cancellation) =>
         !await _studentService.IsNameExistAsync(name))
     .WithMessage("Student name already exists");
        }
    }
}
