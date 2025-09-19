using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required.");
            RuleFor(x => x.Dto.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.")
                .When(x => x.Dto.Amount.HasValue);
            RuleFor(x => x.Dto.Currency)
                .Length(3).WithMessage("Currency code must be 3 characters long (e.g., USD, EUR).")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Currency));
            RuleFor(x => x.Dto.PaymentMethod)
                .MaximumLength(50).WithMessage("Payment method cannot exceed 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.PaymentMethod));
            RuleFor(x => x.Dto.TransactionId)
                .MaximumLength(256).WithMessage("Transaction ID cannot exceed 256 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.TransactionId));
            RuleFor(x => x.Dto.Status)
                .IsInEnum().WithMessage("Invalid payment status.")
                .When(x => x.Dto.Status.HasValue);
            RuleFor(x => x.Dto.Remarks)
                .MaximumLength(500).WithMessage("Remarks cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Remarks));
        }
    }
}
