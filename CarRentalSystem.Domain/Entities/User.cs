using CarRentalSystem.Domain.Enums;
using CarRentalSystem.Domain.ValueObjects;
using System.Net;

namespace CarRentalSystem.Domain.Entities;

public class User: AuditableEntity
{
    //public int UserId { get; set; }

    //public string FullName { get; set; } = string.Empty;
    //public string Email { get; set; } = string.Empty;
    //public string Password { get; set; } = string.Empty;   // hashed
    //public string Phone { get; set; } = string.Empty;
    //public string Role { get; set; } = "Customer";
    //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //public bool EmailConfirmed { get; set; } = false;
    //public string? GoogleId { get; set; }

    //public Address? Address { get; set; }

    // Navigation properties


    // Login identifiers

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Address? Address { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string? Username { get; set; }

    // Security (hashing in Application/Infrastructure; plain password never stored)
    public string PasswordHash { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!; // (PBKDF2/BCrypt/Argon2 salt as base64)

    // Status
    public bool IsActive { get; set; } = true;
    public bool EmailConfirmed { get; set; } = false;
    public DateTime? LastLoginUtc { get; set; }

    // Roles (simple): primary role + optional multi-role join in future
    public UserRole Role { get; set; } = UserRole.Customer;

    // External providers (optional)
    public string? GoogleSubjectId { get; set; }  // sub from Google ID token
    public string? GoogleEmail { get; set; }      // verified email at signup

    // Navigation (optional 1:1)
    public Staff? StaffProfile { get; set; }
    public Customer? CustomerProfile { get; set; }




}