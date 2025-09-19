using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Repositories
{
    internal sealed class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _ctx;

        public PaymentRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Payment> Query()
        {
            return _ctx.Payments.AsQueryable();
        }

        public async Task<Payment?> FindByTransactionIdAsync(string transactionId, CancellationToken ct = default)
        {
            return await _ctx.Payments
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId, ct);
        }

        public async Task<bool> ExistsByTransactionIdAsync(string transactionId, CancellationToken ct = default)
        {
            return await _ctx.Payments
                .AnyAsync(p => p.TransactionId == transactionId, ct);
        }
    }
}
