using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.Dto.CarId).NotEmpty().WithMessage("CarId is required.");
            RuleFor(x => x.Dto.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.Dto.PickUpDate)
                .NotEmpty().WithMessage("Pick up date is required.")
                .Must(date => date > DateTime.UtcNow.AddMinutes(-5))
                .WithMessage("Pick up date must be in the future.");
            RuleFor(x => x.Dto.ReturnDate)
                .NotEmpty().WithMessage("Return date is required.")
                .GreaterThan(x => x.Dto.PickUpDate)
                .WithMessage("Return date must be after pick up date.");
        }
    }
}
