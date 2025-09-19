using CarRentalSystem.Application.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarStatus
{
    public class SoftDeleteCarStatusCommandValidator : AbstractValidator<SoftDeleteCarStatusCommand>
    {
        public SoftDeleteCarStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Car status Id is required.");
        }
    }
}
