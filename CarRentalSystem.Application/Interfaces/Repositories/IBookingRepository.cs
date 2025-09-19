using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IQueryable<Booking> Query();

        Task<Booking?> FindByCarIdAsync(Guid carId, CancellationToken ct = default);

        Task<List<Booking>> FindByCustomerIdAsync(Guid customerId, CancellationToken ct = default);
    }
}
