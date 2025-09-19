using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Payment;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Payment
{
    public sealed class GetPaymentsPagedQueryHandler
       : IRequestHandler<GetPaymentsPagedQuery, PagedResultDto<PaymentDto>>
    {
        private readonly IPaymentRepository _payments;
        private readonly IMapper _mapper;

        public GetPaymentsPagedQueryHandler(IPaymentRepository payments, IMapper mapper)
        {
            _payments = payments;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<PaymentDto>> Handle(GetPaymentsPagedQuery request, CancellationToken ct)
        {
            var query = _payments.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(p => p.PaymentDate) // Order by latest payments first
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<PaymentDto>(items, total, request.Page, request.PageSize);
        }
    }
}
