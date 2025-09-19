using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Car
{
    public class SoftDeleteCarCommandValidator : AbstractValidator<SoftDeleteCarCommand>
    {
        public SoftDeleteCarCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Car Id is required.");
        }
    }
}
