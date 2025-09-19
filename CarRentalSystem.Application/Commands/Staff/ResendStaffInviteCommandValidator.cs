using FluentValidation;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class ResendStaffInviteCommandValidator : AbstractValidator<ResendStaffInviteCommand>
    {
        public ResendStaffInviteCommandValidator()
        {
            RuleFor(x => x.InviteId)
                .NotEmpty().WithMessage("InviteId is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.RequestedBy)
                .NotEmpty().WithMessage("RequestedBy is required.")
                .MaximumLength(100);
        }
    }
}