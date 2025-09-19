using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Car
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.Dto.CarModelId).NotEmpty();
            RuleFor(x => x.Dto.CarStatusId).NotEmpty();
            RuleFor(x => x.Dto.PlateNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Dto.Color).MaximumLength(50);
            RuleFor(x => x.Dto.Mileage).GreaterThanOrEqualTo(0);
        }
    }
}
