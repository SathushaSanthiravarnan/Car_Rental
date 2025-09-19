using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.ForgotPassword
{
    public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
        }
    }
}