using FluentValidation;

namespace CarRentalSystem.Application.Queries.Auth.ConfirmEmail
{
    public sealed class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Token).NotEmpty().MinimumLength(16);
        }
    }
}