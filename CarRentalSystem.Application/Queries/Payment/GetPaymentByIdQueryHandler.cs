using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
    {
        private readonly IPaymentRepository _payments;
        private readonly IMapper _mapper;

        public GetPaymentByIdQueryHandler(IPaymentRepository payments, IMapper mapper)
        {
            _payments = payments;
            _mapper = mapper;
        }

        public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken ct)
        {
            return await _payments.Query()
                .Where(p => p.Id == request.PaymentId)
                .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
