using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Manufacturer
{
    public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
    {
        public CreateManufacturerCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.Country)
                .MaximumLength(100);

            RuleFor(x => x.Dto.FoundedYear)
                .InclusiveBetween(1000, DateTime.UtcNow.Year)
                .When(x => x.Dto.FoundedYear.HasValue)
                .WithMessage("Founded Year must be a valid year.");
        }
    }
}
