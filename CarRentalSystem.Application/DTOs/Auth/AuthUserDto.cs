using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Auth
{
    public sealed record AuthUserDto(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role // keep as string (even if domain uses enum)
    );
}