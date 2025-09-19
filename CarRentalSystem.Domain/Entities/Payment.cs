using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public string? Remarks { get; set; }
    }
}
