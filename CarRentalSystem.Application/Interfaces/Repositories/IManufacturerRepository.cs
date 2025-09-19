using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        /// <summary>
        /// Provides a queryable access to the Manufacturer entities for LINQ operations.
        /// </summary>
        IQueryable<Manufacturer> Query();

        /// <summary>
        /// Finds a manufacturer by its name, case-insensitively.
        /// </summary>
        /// <param name="name">The name of the manufacturer to find.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The Manufacturer entity if found, otherwise null.</returns>
        Task<Manufacturer?> FindByNameAsync(string name, CancellationToken ct = default);
    }
}
