using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.Dto.BookingId)
                .NotEmpty().WithMessage("Booking ID is required.");
            RuleFor(x => x.Dto.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.Dto.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency code must be 3 characters long (e.g., USD, EUR).");
            RuleFor(x => x.Dto.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .MaximumLength(50).WithMessage("Payment method cannot exceed 50 characters.");
            RuleFor(x => x.Dto.TransactionId)
                .NotEmpty().WithMessage("Transaction ID is required.")
                .MaximumLength(256).WithMessage("Transaction ID cannot exceed 256 characters.");
            RuleFor(x => x.Dto.Remarks)
                .MaximumLength(500).WithMessage("Remarks cannot exceed 500 characters.");
        }
    }
}
