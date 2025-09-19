using CarRentalSystem.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Application.Commands.Auth.RegisterGuest
{
    public sealed class RegisterGuestCommandValidator : AbstractValidator<RegisterGuestCommand>
    {
        public RegisterGuestCommandValidator(IUserRepository users)
        {
            RuleFor(x => x.Dto.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Dto.LastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Dto.Email)
                .NotEmpty().EmailAddress()
                .MustAsync(async (email, ct) =>
                    !await users.Query().AnyAsync(u => u.Email == email, ct))
                .WithMessage("Email already registered.");

            RuleFor(x => x.Dto.Password)
                .NotEmpty().MinimumLength(6);
        }
    }
}