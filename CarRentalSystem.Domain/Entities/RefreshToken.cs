using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class RefreshToken : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAtUtc { get; set; }
        public string? CreatedByIp { get; set; }
        public string? UserAgent { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        // Helper property (not mapped in EF by default)
        public bool IsActive => RevokedAtUtc == null && DateTime.UtcNow < ExpiresAtUtc;

        // Navigation property
        public User? User { get; set; }
    }
}