using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface ICarStatusRepository : IRepository<CarStatus>
    {
        IQueryable<CarStatus> Query();
        Task<CarStatus?> FindByNameAsync(string name, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    }
}
