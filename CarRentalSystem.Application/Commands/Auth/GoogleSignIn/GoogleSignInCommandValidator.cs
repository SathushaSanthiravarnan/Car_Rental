using FluentValidation;

namespace CarRentalSystem.Application.Commands.Auth.GoogleSignIn
{
    public sealed class GoogleSignInCommandValidator : AbstractValidator<GoogleSignInCommand>
    {
        public GoogleSignInCommandValidator()
        {
            RuleFor(x => x.Dto.IdToken).NotEmpty().MinimumLength(20);
        }
    }
}