using AutoMapper;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class RefreshStaffInviteTokenCommandHandler
        : IRequestHandler<RefreshStaffInviteTokenCommand, StaffInviteDto>
    {
        private readonly IStaffInviteRepository _invites;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RefreshStaffInviteTokenCommandHandler(
            IStaffInviteRepository invites,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _invites = invites;
            _db = db;
            _mapper = mapper;
        }

        public async Task<StaffInviteDto> Handle(RefreshStaffInviteTokenCommand request, CancellationToken ct)
        {
            var invite = await _invites.Query()
                .FirstOrDefaultAsync(i => i.Id == request.InviteId && !i.IsUsed, ct);

            if (invite is null)
                throw new InvalidOperationException("Invite not found or already used.");

            // Generate a new secure token
            Span<byte> bytes = stackalloc byte[32];
            RandomNumberGenerator.Fill(bytes);
            invite.Token = WebEncoders.Base64UrlEncode(bytes);
            invite.ExpiresAt = DateTime.UtcNow.AddDays(7); // reset expiry
            invite.IsUsed = false;

            // Optional: audit
            // invite.LastRefreshedBy = request.RequestedBy;
            // invite.LastRefreshedAt = DateTime.UtcNow;

            _invites.Update(invite);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StaffInviteDto>(invite);
        }
    }
}