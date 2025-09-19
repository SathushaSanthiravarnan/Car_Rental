using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class StaffInvite : AuditableEntity
    {        
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // URL-safe random token
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
