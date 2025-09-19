using AutoMapper;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Notifications;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CarRentalSystem.Application.Commands.Staff;

public sealed class CreateStaffInviteCommandHandler : IRequestHandler<CreateStaffInviteCommand, StaffInviteDto>
{
    private readonly IStaffInviteRepository _invites;
    private readonly IApplicationDbContext _db;
    private readonly IEmailSender _email;
    private readonly IMapper _mapper;

    public CreateStaffInviteCommandHandler(IStaffInviteRepository invites, IApplicationDbContext db, IEmailSender email, IMapper mapper)
    { _invites = invites; _db = db; _email = email; _mapper = mapper; }

    public async Task<StaffInviteDto> Handle(CreateStaffInviteCommand request, CancellationToken ct)
    {
        // if there is an active invite already, reuse/refresh
        var existing = await _invites.Query()
            .FirstOrDefaultAsync(i => i.Email == request.Dto.Email && !i.IsUsed && i.ExpiresAt > DateTime.UtcNow, ct);

        var invite = existing ?? new StaffInvite { Email = request.Dto.Email };

        // generate secure token
        Span<byte> bytes = stackalloc byte[32];
        RandomNumberGenerator.Fill(bytes);
        invite.Token = WebEncoders.Base64UrlEncode(bytes);
        invite.ExpiresAt = DateTime.UtcNow.AddDays(7);
        invite.IsUsed = false;

        if (existing is null) await _invites.AddAsync(invite, ct);
        else _invites.Update(invite);

        await _db.SaveChangesAsync(ct);

        // send email (link example)
        var link = $"https://your-frontend.example.com/staff/accept-invite?token={invite.Token}";
        await _email.SendEmailAsync(request.Dto.Email, "Your staff invitation",
            $"Please complete your registration: {link}", ct);

        return _mapper.Map<StaffInviteDto>(invite);
    }
}