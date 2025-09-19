using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
    public class SoftDeletePaymentCommandValidator : AbstractValidator<SoftDeletePaymentCommand>
    {
        public SoftDeletePaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required for soft deletion.");
        }
    }
}
