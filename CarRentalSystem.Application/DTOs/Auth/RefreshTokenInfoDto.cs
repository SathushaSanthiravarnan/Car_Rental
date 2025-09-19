using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Auth
{
    public sealed record RefreshTokenInfoDto(
        string TokenPreview,   // e.g. first 6 chars only
        DateTime ExpiresAtUtc,
        bool IsActive,
        string? UserAgent,
        string? CreatedByIp
    );
}