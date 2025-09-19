using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.Dto.DrivingLicenseExpiryUtc)
                .GreaterThan(DateTime.UtcNow).WithMessage("Driving license must not be expired.")
                .When(x => x.Dto.DrivingLicenseExpiryUtc.HasValue);

            RuleFor(x => x.Dto.NationalIdNumber)
                .MaximumLength(50);

            RuleFor(x => x.Dto.DrivingLicenseNumber)
                .MaximumLength(50);

            RuleFor(x => x.Dto.AlternatePhone)
                .MaximumLength(25)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.AlternatePhone))
                .WithMessage("Alternate phone must be digits with optional +, space, -, ()");

            RuleFor(x => x.Dto.EmailForContact)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.EmailForContact));
        }
    }
}
