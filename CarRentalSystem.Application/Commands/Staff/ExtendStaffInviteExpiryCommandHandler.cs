using AutoMapper;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class ExtendStaffInviteExpiryCommandHandler
        : IRequestHandler<ExtendStaffInviteExpiryCommand, StaffInviteDto>
    {
        private readonly IStaffInviteRepository _invites;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ExtendStaffInviteExpiryCommandHandler(
            IStaffInviteRepository invites,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _invites = invites;
            _db = db;
            _mapper = mapper;
        }

        public async Task<StaffInviteDto> Handle(
            ExtendStaffInviteExpiryCommand request,
            CancellationToken ct)
        {
            var invite = await _invites.Query()
                .FirstOrDefaultAsync(i => i.Id == request.InviteId, ct);

            if (invite is null)
                throw new InvalidOperationException("Invite not found.");

            if (invite.IsUsed)
                throw new InvalidOperationException("Cannot extend an already used invite.");

            if (request.NewExpiryUtc <= DateTime.UtcNow)
                throw new InvalidOperationException("New expiry must be a future date.");

            // Update expiry
            invite.ExpiresAt = request.NewExpiryUtc;

            // Optional: audit log or metadata about who extended it
            // e.g., invite.UpdatedBy = request.RequestedBy;

            _invites.Update(invite);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StaffInviteDto>(invite);
        }
    }
}