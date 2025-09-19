using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.ResetPassword
{
    public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Dto.Token).NotEmpty().MinimumLength(16);
            RuleFor(x => x.Dto.NewPassword)
                .NotEmpty().MinimumLength(6)
                .Must(p => !p.Contains(' '))
                .WithMessage("Password cannot contain spaces.");
        }
    }
}