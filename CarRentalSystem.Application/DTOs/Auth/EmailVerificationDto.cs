using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Auth
{
    public sealed record EmailVerificationDto(
        Guid Id,
        Guid UserId,
        string TokenPreview,
        DateTime ExpiresAt,
        bool IsUsed
    );

}