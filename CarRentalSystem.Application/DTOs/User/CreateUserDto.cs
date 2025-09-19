using CarRentalSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.User
{
    public sealed record CreateUserDto(
        string FirstName,
        string LastName,
        string Email,
        string? Phone,
        string? Username,
        AddressDto? Address,
        string? Password,
        string Role
    );
}
