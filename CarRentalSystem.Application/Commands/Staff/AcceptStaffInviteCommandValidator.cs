using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public class AcceptStaffInviteCommandValidator : AbstractValidator<AcceptStaffInviteCommand>
    {
        public AcceptStaffInviteCommandValidator()
        {
            RuleFor(x => x.Dto.Token)
                .NotEmpty().WithMessage("Invite token is required.");

            RuleFor(x => x.Dto.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
            // Add extra rules if you want (upper/lower/number/special)
            // .Matches("[A-Z]").WithMessage("One uppercase required.")
            // .Matches("[a-z]").WithMessage("One lowercase required.")
            // .Matches("[0-9]").WithMessage("One digit required.")
            // .Matches(@"[\W_]").WithMessage("One special character required.");
        }
    }
}
