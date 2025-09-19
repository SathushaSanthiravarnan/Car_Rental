using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.RefreshToken
{
    public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Dto.RefreshToken).NotEmpty().MinimumLength(32);
        }
    }
}