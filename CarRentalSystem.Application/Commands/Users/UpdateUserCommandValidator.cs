using CarRentalSystem.Application.DTOs.Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            // UserId must be provided
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            // First/Last names are required in your DTO (non-nullable)
            RuleFor(x => x.Dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            // Username (optional, format if provided)
            RuleFor(x => x.Dto.Username)
                .MaximumLength(50)
                .Matches(@"^[a-zA-Z0-9_.-]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Username))
                .WithMessage("Username can only contain letters, numbers, ., _, -");

            // Phone (optional, format if provided)
            RuleFor(x => x.Dto.Phone)
                .MaximumLength(25)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Phone))
                .WithMessage("Phone must be digits with optional +, space, -, ()");

            // Address validation (if provided)
            RuleFor(x => x.Dto.Address)
                .SetValidator(new AddressDtoValidator())
                .When(x => x.Dto.Address is not null);
        }
    }

    /// <summary>
    /// Nested validator for AddressDto.
    /// </summary>
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(a => a.Street).MaximumLength(200);
            RuleFor(a => a.City).MaximumLength(100);
            RuleFor(a => a.State).MaximumLength(100);
            RuleFor(a => a.PostalCode).MaximumLength(20);
            RuleFor(a => a.Country).MaximumLength(100);
        }
    }
}
