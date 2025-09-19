using CarRentalSystem.Application.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarStatus
{
    public class CreateCarStatusCommandValidator : AbstractValidator<CreateCarStatusCommand>
    {
        public CreateCarStatusCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Car status name is required.")
                .MaximumLength(50).WithMessage("Car status name cannot exceed 50 characters.");

            RuleFor(x => x.Dto.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }
    }
}
