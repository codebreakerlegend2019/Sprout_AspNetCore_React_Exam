using FluentValidation;
using Sprout.Exam.DataAccess.Dtos;

namespace Sprout.Exam.DataAccess.Validators
{
    public class EditEmployeeValidator: AbstractValidator<EmployeeEditDto>
    {
        public EditEmployeeValidator()
        {
            RuleFor(e => e.Birthdate)
              .NotNull();

            RuleFor(e => e.FullName)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.Tin)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.EmployeeTypeId)
                .LessThanOrEqualTo(2)
                .GreaterThanOrEqualTo(1)
                .NotEqual(0);
        }
    }
}
