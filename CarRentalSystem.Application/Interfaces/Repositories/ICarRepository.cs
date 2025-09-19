using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
        IQueryable<Car> Query();
        Task<Car?> FindByPlateNumberAsync(string plateNumber, CancellationToken ct = default);
        Task<bool> ExistsByPlateNumberAsync(string plateNumber, CancellationToken ct = default);
    }
}
