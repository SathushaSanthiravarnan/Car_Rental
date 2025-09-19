using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public class SoftDeleteBookingCommandValidator : AbstractValidator<SoftDeleteBookingCommand>
    {
        public SoftDeleteBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("Booking ID is required.");
        }
    }
}
