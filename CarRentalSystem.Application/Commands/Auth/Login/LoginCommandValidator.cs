using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Dto.Password).NotEmpty();
        }
    }
}