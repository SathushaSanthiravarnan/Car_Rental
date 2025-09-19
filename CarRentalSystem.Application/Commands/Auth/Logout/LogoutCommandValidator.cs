using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.Logout
{
    public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.Dto.RefreshToken).NotEmpty().MinimumLength(32);
        }
    }
}