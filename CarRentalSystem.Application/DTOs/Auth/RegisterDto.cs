using CarRentalSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Auth
{
    public sealed record RegisterDto(
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
