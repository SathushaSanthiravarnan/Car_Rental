using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("Booking ID is required.");

            When(x => x.Dto.PickUpDate.HasValue && x.Dto.ReturnDate.HasValue, () =>
            {
                RuleFor(x => x.Dto.ReturnDate)
                    .GreaterThan(x => x.Dto.PickUpDate.Value)
                    .WithMessage("Return date must be after pick up date.");
            });

            When(x => x.Dto.PickUpDate.HasValue, () =>
            {
                RuleFor(x => x.Dto.PickUpDate)
                    .GreaterThan(DateTime.UtcNow.AddMinutes(-5))
                    .WithMessage("Pick up date must be in the future.");
            });
        }
    }
}
