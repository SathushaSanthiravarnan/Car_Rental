using CarRentalSystem.Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Payment
{
    public sealed record GetPaymentByIdQuery(Guid PaymentId) : IRequest<PaymentDto?>;
}
