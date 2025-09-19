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
    internal sealed class CarModelRepository : RepositoryBase<CarModel>, ICarModelRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CarModelRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<CarModel> Query() => _ctx.CarModels.AsQueryable();

        public async Task<CarModel?> FindByNameAsync(Guid manufacturerId, string name, CancellationToken ct = default)
        {
            return await _ctx.CarModels
                .FirstOrDefaultAsync(c => c.ManufacturerId == manufacturerId && c.Name == name, ct);
        }
    }
}
