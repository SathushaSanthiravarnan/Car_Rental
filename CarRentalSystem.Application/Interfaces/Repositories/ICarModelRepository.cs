using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface ICarModelRepository : IRepository<CarModel>
    {
        IQueryable<CarModel> Query();
        Task<CarModel?> FindByNameAsync(Guid manufacturerId, string name, CancellationToken ct = default);
    }
}
