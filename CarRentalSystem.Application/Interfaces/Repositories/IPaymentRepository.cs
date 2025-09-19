using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        
        IQueryable<Payment> Query();

      
        /// <param name="transactionId">The transaction ID to search for.</param>
        /// <param name="ct">The cancellation token.</param>
        Task<Payment?> FindByTransactionIdAsync(string transactionId, CancellationToken ct = default);

        /// <summary>
        /// Checks if a payment with a given transaction ID already exists.
        /// </summary>
        /// <param name="transactionId">The transaction ID to check.</param>
        /// <param name="ct">The cancellation token.</param>
        Task<bool> ExistsByTransactionIdAsync(string transactionId, CancellationToken ct = default);
    }
}
