using AutoMapper;
using CarRentalSystem.Application.Commands.Staff;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using CarRentalSystem.Domain.Entities;
using MediatR;

public sealed class AcceptStaffInviteCommandHandler
    : IRequestHandler<AcceptStaffInviteCommand, StaffDto>
{
    private readonly IStaffInviteRepository _invites;
    private readonly IUserRepository _users;
    private readonly IStaffRepository _staff;
    private readonly IApplicationDbContext _db;
    private readonly IPasswordHasher? _hasher;
    private readonly IMapper _mapper;

    public AcceptStaffInviteCommandHandler(
        IStaffInviteRepository invites,
        IUserRepository users,
        IStaffRepository staff,
        IApplicationDbContext db,
        IPasswordHasher? hasher,
        IMapper mapper)
    {
        _invites = invites; _users = users; _staff = staff;
        _db = db; _hasher = hasher; _mapper = mapper;
    }

    public async Task<StaffDto> Handle(AcceptStaffInviteCommand request, CancellationToken ct)
    {
        var invite = await _invites.FindByTokenAsync(request.Dto.Token, ct)
                     ?? throw new InvalidOperationException("Invalid invite token.");
        if (invite.IsUsed || invite.ExpiresAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Invite expired or already used.");

        if (_hasher is null)
            throw new InvalidOperationException("Password hasher not configured.");

        var (hash, salt) = _hasher.Hash(request.Dto.Password);

        var user = new User
        {
            Email = invite.Email,
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            PasswordHash = hash,
            PasswordSalt = salt,
            EmailConfirmed = true,
            IsActive = true,
            Role = CarRentalSystem.Domain.Enums.UserRole.Staff
        };
        await _users.AddAsync(user, ct);

        var staff = new CarRentalSystem.Domain.Entities.Staff
        {
            User = user,
            JoinedOnUtc = DateTime.UtcNow,
            IsActive = true
        };
        await _staff.AddAsync(staff, ct);

        invite.IsUsed = true;
        _invites.Update(invite);

        await _db.SaveChangesAsync(ct);

        return _mapper.Map<StaffDto>(staff);

    }
}