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
    internal sealed class CarStatusRepository : RepositoryBase<CarStatus>, ICarStatusRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CarStatusRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<CarStatus> Query() => _ctx.CarStatuses.AsQueryable();

        public async Task<CarStatus?> FindByNameAsync(string name, CancellationToken ct = default)
        {
            return await _ctx.CarStatuses.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower(), ct);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        {
            return await _ctx.CarStatuses.AnyAsync(s => s.Name.ToLower() == name.ToLower(), ct);
        }
    }
}
