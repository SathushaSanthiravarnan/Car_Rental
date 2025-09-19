using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;

namespace CarRentalSystem.Application.Queries.Auth.ConfirmEmail
{
    public sealed class ConfirmEmailQueryHandler : IRequestHandler<ConfirmEmailQuery, bool>
    {
        private readonly IUserRepository _users;
        private readonly IEmailVerificationRepository _verifications;

        public ConfirmEmailQueryHandler(IUserRepository users, IEmailVerificationRepository verifications)
        {
            _users = users; _verifications = verifications;
        }

        public async Task<bool> Handle(ConfirmEmailQuery request, CancellationToken ct)
        {
            var user = await _users.FindByEmailAsync(request.Email, ct)
                       ?? throw new InvalidOperationException("Invalid email.");

            var v = await _verifications.FindByUserAndTokenAsync(user.Id, request.Token, ct)
                    ?? throw new InvalidOperationException("Invalid token.");

            if (v.IsUsed || v.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException("Token expired or already used.");

            v.IsUsed = true;
            user.EmailConfirmed = true;

            _verifications.Update(v);
            await _verifications.SaveChangesAsync(ct);
            return true;
        }
    }
}