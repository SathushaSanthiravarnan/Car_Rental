using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Payment
{
    public sealed record GetPaymentsPagedQuery(int Page = 1, int PageSize = 20)
        : IRequest<PagedResultDto<PaymentDto>>;
}
