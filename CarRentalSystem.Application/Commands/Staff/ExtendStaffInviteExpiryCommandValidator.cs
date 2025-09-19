using FluentValidation;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class ExtendStaffInviteExpiryCommandValidator
        : AbstractValidator<ExtendStaffInviteExpiryCommand>
    {
        public ExtendStaffInviteExpiryCommandValidator()
        {
            // InviteId must be present
            RuleFor(x => x.InviteId)
                .NotEmpty().WithMessage("Invite id is required.");

            // New expiry must be in the future (UTC)
            RuleFor(x => x.NewExpiryUtc)
                .Must(expiry => expiry > DateTime.UtcNow)
                .WithMessage("New expiry must be in the future (UTC).")
                .Must(expiry => expiry <= DateTime.UtcNow.AddDays(180))
                .WithMessage("New expiry cannot be more than 180 days from now.");

            // RequestedBy must be present
            RuleFor(x => x.RequestedBy)
                .NotEmpty().WithMessage("RequestedBy is required.")
                .MaximumLength(256);
        }
    }
}