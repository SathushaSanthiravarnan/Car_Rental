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
    internal sealed class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CarRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Car> Query() => _ctx.Cars.AsQueryable();

        public async Task<Car?> FindByPlateNumberAsync(string plateNumber, CancellationToken ct = default)
        {
            return await _ctx.Cars.FirstOrDefaultAsync(c => c.PlateNumber.ToLower() == plateNumber.ToLower(), ct);
        }

        public async Task<bool> ExistsByPlateNumberAsync(string plateNumber, CancellationToken ct = default)
        {
            return await _ctx.Cars.AnyAsync(c => c.PlateNumber.ToLower() == plateNumber.ToLower(), ct);
        }
    }
}
