using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Manufacturer
{
    public class SoftDeleteManufacturerCommandValidator : AbstractValidator<SoftDeleteManufacturerCommand>
    {
        public SoftDeleteManufacturerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Manufacturer ID is required.");
        }
    }
}
