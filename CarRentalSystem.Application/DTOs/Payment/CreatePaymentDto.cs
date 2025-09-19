using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Payment
{
    public sealed record CreatePaymentDto(
        Guid BookingId,
        decimal Amount,
        string Currency,
        string PaymentMethod,
        string TransactionId,
        string? Remarks
    );
}
