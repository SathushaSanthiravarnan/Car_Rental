using AutoMapper;
using CarRentalSystem.Application.Interfaces.Notifications;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class ResendStaffInviteCommandHandler : IRequestHandler<ResendStaffInviteCommand, bool>
    {
        private readonly IStaffInviteRepository _invites;
        private readonly IApplicationDbContext _db;
        private readonly IEmailSender _email;

        public ResendStaffInviteCommandHandler(
            IStaffInviteRepository invites,
            IApplicationDbContext db,
            IEmailSender email)
        {
            _invites = invites;
            _db = db;
            _email = email;
        }

        public async Task<bool> Handle(ResendStaffInviteCommand request, CancellationToken ct)
        {
            var invite = await _invites.Query()
                .FirstOrDefaultAsync(i => i.Id == request.InviteId && !i.IsUsed, ct);

            if (invite is null || invite.ExpiresAt < DateTime.UtcNow)
                return false; // invalid or expired

            // build frontend link
            var link = $"https://your-frontend.example.com/staff/accept-invite?token={invite.Token}";

            await _email.SendEmailAsync(
                request.Email,
                "Resend: Your staff invitation",
                $"Please complete your registration: {link}",
                ct);

            // optional audit log
            // invite.LastResentBy = request.RequestedBy;
            // invite.LastResentAt = DateTime.UtcNow;
            _invites.Update(invite);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}