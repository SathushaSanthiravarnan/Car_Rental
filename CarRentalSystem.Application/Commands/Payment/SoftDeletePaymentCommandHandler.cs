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
    public sealed class SoftDeletePaymentCommandHandler : IRequestHandler<SoftDeletePaymentCommand, bool>
    {
        private readonly IPaymentRepository _payments;
        private readonly IApplicationDbContext _db;

        public SoftDeletePaymentCommandHandler(IPaymentRepository payments, IApplicationDbContext db)
        {
            _payments = payments;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeletePaymentCommand request, CancellationToken ct)
        {
            var payment = await _payments.GetByIdAsync(request.PaymentId, ct);
            if (payment is null)
            {
                return false;
            }

            payment.IsDeleted = true;
            _payments.Update(payment);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }

}
