using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.ResendVerification
{
    public sealed class ResendVerificationCommandValidator : AbstractValidator<ResendVerificationCommand>
    {
        public ResendVerificationCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
        }
    }
}