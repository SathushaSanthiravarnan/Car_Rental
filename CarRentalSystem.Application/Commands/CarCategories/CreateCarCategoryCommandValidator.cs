using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategory
{
    public class CreateCarCategoryCommandValidator : AbstractValidator<CreateCarCategoryCommand>
    {
        public CreateCarCategoryCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.DailyRateModifier)
                .GreaterThan(0).WithMessage("Daily rate modifier must be greater than zero.");
        }
    }
}
