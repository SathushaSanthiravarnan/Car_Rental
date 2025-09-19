using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Payment
{
    public sealed record PaymentDto
    {
        public Guid Id { get; init; }
        public Guid BookingId { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = default!;
        public string PaymentMethod { get; init; } = default!;
        public string TransactionId { get; init; } = default!;
        public DateTime PaymentDate { get; init; }
        public PaymentStatus Status { get; init; }
        public string? Remarks { get; init; }
    }
}
