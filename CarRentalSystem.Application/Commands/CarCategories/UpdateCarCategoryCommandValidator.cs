using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategory
{
    public class UpdateCarCategoryCommandValidator : AbstractValidator<UpdateCarCategoryCommand>
    {
        public UpdateCarCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Car category ID is required.");

            RuleFor(x => x.Dto.Name)
                .MaximumLength(100);

            RuleFor(x => x.Dto.DailyRateModifier)
                .GreaterThan(0).When(x => x.Dto.DailyRateModifier.HasValue)
                .WithMessage("Daily rate modifier must be greater than zero.");
        }
    }
}
