using AutoMapper;
using CarRentalSystem.Application.DTOs.Payment;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
    public sealed class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, PaymentDto>
    {
        private readonly IPaymentRepository _payments;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdatePaymentCommandHandler(IPaymentRepository payments, IApplicationDbContext db, IMapper mapper)
        {
            _payments = payments;
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaymentDto> Handle(UpdatePaymentCommand request, CancellationToken ct)
        {
            var payment = await _payments.GetByIdAsync(request.PaymentId, ct)
                ?? throw new KeyNotFoundException($"Payment with ID {request.PaymentId} not found.");

            _mapper.Map(request.Dto, payment);

            _payments.Update(payment);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<PaymentDto>(payment);
        }
    }
}
