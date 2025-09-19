using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public class CreateCarModelCommandValidator : AbstractValidator<CreateCarModelCommand>
    {
        public CreateCarModelCommandValidator()
        {
            RuleFor(x => x.Dto.ManufacturerId)
                .NotEmpty().WithMessage("Manufacturer ID is required.");

            RuleFor(x => x.Dto.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.BodyType)
                .MaximumLength(50);

            RuleFor(x => x.Dto.FuelType)
                .MaximumLength(50);

            RuleFor(x => x.Dto.Transmission)
                .MaximumLength(50);
        }
    }
}
