using FluentValidation;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class RefreshStaffInviteTokenCommandValidator
        : AbstractValidator<RefreshStaffInviteTokenCommand>
    {
        public RefreshStaffInviteTokenCommandValidator()
        {
            RuleFor(x => x.InviteId)
                .NotEmpty().WithMessage("InviteId is required.");

            RuleFor(x => x.RequestedBy)
                .NotEmpty().WithMessage("RequestedBy is required.")
                .MaximumLength(100);
        }
    }
}