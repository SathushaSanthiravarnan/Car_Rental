using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IInspectionRepository : IRepository<Inspection>
    {
        IQueryable<Inspection> Query();
        Task<Inspection?> FindByBookingIdAsync(Guid bookingId, CancellationToken ct = default);
        Task<bool> ExistsByBookingIdAsync(Guid bookingId, CancellationToken ct = default);
    }
}
