using AutoMapper;
using CarRentalSystem.Application.Commands.Auth.RegisterGuest;
using CarRentalSystem.Application.Interfaces.Notifications;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

public sealed class RegisterGuestCommandHandler : IRequestHandler<RegisterGuestCommand, Guid>
{
    private readonly IUserRepository _users;
    private readonly ICustomerRepository _customers;
    private readonly IEmailVerificationRepository _verifications;
    private readonly IPasswordHasher _hasher;
    private readonly IEmailSender _email;
    private readonly IConfiguration _cfg;
    private readonly IMapper _mapper;

    public RegisterGuestCommandHandler(
        IUserRepository users,
        ICustomerRepository customers,
        IEmailVerificationRepository verifications,
        IPasswordHasher hasher,
        IEmailSender email,
        IConfiguration cfg,
        IMapper mapper)
    {
        _users = users; _customers = customers; _verifications = verifications;
        _hasher = hasher; _email = email; _cfg = cfg; _mapper = mapper;
    }

    public async Task<Guid> Handle(RegisterGuestCommand request, CancellationToken ct)
    {
        var user = _mapper.Map<User>(request.Dto);
        var (hash, salt) = _hasher.Hash(request.Dto.Password);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.Role = UserRole.Customer;

        await _users.AddAsync(user, ct);
        await _customers.AddAsync(new Customer { UserId = user.Id }, ct);

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        await _verifications.AddAsync(new EmailVerification
        {
            UserId = user.Id,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            IsUsed = false
        }, ct);
        await _verifications.SaveChangesAsync(ct);

        var fe = _cfg["Email:FrontendBaseUrl"];
        var link = $"{fe}/auth/verify?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";
        await _email.SendEmailAsync(user.Email, "Confirm your email",
            $"Please confirm your email by clicking <a href=\"{link}\">this link</a>.");

        return user.Id;
    }
}