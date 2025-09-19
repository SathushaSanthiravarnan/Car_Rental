using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.TestDrive
{
    public sealed class CreateTestDriveCommandValidator : AbstractValidator<CreateTestDriveCommand>
    {
        public CreateTestDriveCommandValidator()
        {
            RuleFor(x => x.Dto.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");

            RuleFor(x => x.Dto.CarId)
                .NotEmpty().WithMessage("Car ID is required.");

            RuleFor(x => x.Dto.PreferredDate)
                .NotEmpty().WithMessage("Preferred date is required.")
                .Must(date => date.Date >= DateTime.UtcNow.Date)
                .WithMessage("Preferred date cannot be in the past.");

            RuleFor(x => x.Dto.PreferredTime)
                .NotEmpty().WithMessage("Preferred time is required.");

            RuleFor(x => x.Dto.DurationMinutes)
                .InclusiveBetween(15, 120).WithMessage("Duration must be between 15 and 120 minutes.");
        }
    }
}
