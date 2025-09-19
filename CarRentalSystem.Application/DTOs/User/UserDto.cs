using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Domain.Enums;

namespace CarRentalSystem.Application.DTOs.User;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string? Phone { get; init; }
    public string? Username { get; init; }
    public AddressDto? Address { get; init; }

    public UserRole Role { get; init; }
    public bool IsActive { get; init; }
    public bool EmailConfirmed { get; init; }
    public DateTime? LastLoginUtc { get; init; }

    public string? GoogleSubjectId { get; init; }
    public string? GoogleEmail { get; init; }
}