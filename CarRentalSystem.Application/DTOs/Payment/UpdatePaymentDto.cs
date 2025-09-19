using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Payment
{
    public sealed record UpdatePaymentDto
    {
        public decimal? Amount { get; init; }
        public string? Currency { get; init; }
        public string? PaymentMethod { get; init; }
        public string? TransactionId { get; init; }
        public PaymentStatus? Status { get; init; }
        public string? Remarks { get; init; }
    }
}
