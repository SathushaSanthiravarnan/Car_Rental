using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class EmailVerification : AuditableEntity
    {       
        public Guid UserId { get; set; }

        public string Token { get; set; } = string.Empty; // URL-safe random token
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        public User? User { get; set; }
    }
}
