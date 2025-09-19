using AutoMapper;
using CarRentalSystem.Application.DTOs.User;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;   // hasher interface here
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Application.Commands.Users
{
    public sealed class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _users;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;

        public CreateUserCommandHandler(
            IUserRepository users,
            IApplicationDbContext db,
            IMapper mapper,
            IPasswordHasher hasher) // not optional
        {
            _users = users;
            _db = db;
            _mapper = mapper;
            _hasher = hasher;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var dto = request.Dto;

            // ---- Uniqueness checks -------------------------------------------------
            var emailNorm = dto.Email.Trim().ToLower();

            var emailExists = await _users.Query()
                .AnyAsync(u => u.Email.ToLower() == emailNorm, ct);
            if (emailExists)
                throw new InvalidOperationException("Email already in use.");

            if (!string.IsNullOrWhiteSpace(dto.Username))
            {
                var unameNorm = dto.Username.Trim().ToLower();
                var usernameExists = await _users.Query()
                    .AnyAsync(u => u.Username != null && u.Username.ToLower() == unameNorm, ct);
                if (usernameExists)
                    throw new InvalidOperationException("Username already in use.");
            }

            // ---- Map DTO → Entity --------------------------------------------------
            var user = _mapper.Map<User>(dto);

            // Role from string (fallback to Customer)
            if (!Enum.TryParse<UserRole>(dto.Role, ignoreCase: true, out var role))
                role = UserRole.Customer;
            user.Role = role;

            // ---- Hash password -----------------------------------------------------
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new InvalidOperationException("Password is required.");

            var (hash, salt) = _hasher.Hash(dto.Password);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            // (Optional) normalize stored email/username
            user.Email = dto.Email.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username.Trim();

            // ---- Persist -----------------------------------------------------------
            await _users.AddAsync(user, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<UserDto>(user);
        }
    }
}
