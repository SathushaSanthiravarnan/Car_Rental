using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            // Email
            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // Password
            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            // First name
            RuleFor(x => x.Dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            // Last name
            RuleFor(x => x.Dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            // Username (optional but constrained if provided)
            RuleFor(x => x.Dto.Username)
                .MaximumLength(50)
                .Matches(@"^[a-zA-Z0-9_.-]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Username))
                .WithMessage("Username can only contain letters, numbers, ., _, -");

            // Phone (optional but format checked if provided)
            RuleFor(x => x.Dto.Phone)
                .MaximumLength(25)
                .Matches(@"^\+?[0-9\s\-()]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.Dto.Phone))
                .WithMessage("Phone must be digits with optional +, space, -, ()");

            // Role (string → must match UserRole enum)
            RuleFor(x => x.Dto.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => Enum.TryParse<Domain.Enums.UserRole>(role, true, out _))
                .WithMessage("Invalid role specified.");
        }
    }
}
