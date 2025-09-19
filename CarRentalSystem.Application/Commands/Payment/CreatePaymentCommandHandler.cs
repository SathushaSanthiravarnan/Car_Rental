using AutoMapper;
using CarRentalSystem.Application.DTOs.Payment;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Payment
{
    public sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
    {
        private readonly IPaymentRepository _payments;
        private readonly IBookingRepository _bookings;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IPaymentRepository payments, IBookingRepository bookings, IApplicationDbContext db, IMapper mapper)
        {
            _payments = payments;
            _bookings = bookings;
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken ct)
        {
            var booking = await _bookings.GetByIdAsync(request.Dto.BookingId, ct)
                ?? throw new KeyNotFoundException("Booking not found.");

            var payment = _mapper.Map<CarRentalSystem.Domain.Entities.Payment>(request.Dto);
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = PaymentStatus.Completed;

            await _payments.AddAsync(payment, ct);

            booking.Status = BookingStatus.Completed;

            await _db.SaveChangesAsync(ct);

            return _mapper.Map<PaymentDto>(payment);
        }
    }
}
