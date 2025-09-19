using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Staff
{
    public sealed record StaffInviteDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = default!;
        public string Token { get; init; } = default!;
        public DateTime ExpiresAt { get; init; }
        public bool IsUsed { get; init; }
    }
}
