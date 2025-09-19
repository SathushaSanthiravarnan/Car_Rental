using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface ICarCategoryRepository : IRepository<CarCategory>
    {
        IQueryable<CarCategory> Query();
        Task<CarCategory?> FindByNameAsync(string name, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default); // The method causing the error
    }
}
