using CarRentalSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.User
{
    public sealed record UpdateUserDto(
        string FirstName,
        string LastName,
        string? Phone,
        string? Username,
        AddressDto? Address,
        bool? IsActive
    );
}
