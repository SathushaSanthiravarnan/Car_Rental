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
    internal sealed class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        private readonly ApplicationDbContext _ctx;

        public ManufacturerRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Provides a queryable access to the Manufacturer entities.
        /// </summary>
        public IQueryable<Manufacturer> Query() => _ctx.Manufacturers.AsQueryable();

        /// <summary>
        /// Finds a manufacturer by its name, case-insensitively.
        /// </summary>
        public async Task<Manufacturer?> FindByNameAsync(string name, CancellationToken ct = default)
        {
            return await _ctx.Manufacturers
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower(), ct);
        }
    }
}
