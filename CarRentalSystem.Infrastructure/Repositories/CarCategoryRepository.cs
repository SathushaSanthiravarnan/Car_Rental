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

    internal sealed class CarCategoryRepository : RepositoryBase<CarCategory>, ICarCategoryRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CarCategoryRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<CarCategory> Query() => _ctx.CarCategories.AsQueryable();

        public async Task<CarCategory?> FindByNameAsync(string name, CancellationToken ct = default)
        {
            return await _ctx.CarCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower(), ct);
        }

        // The missing method is added here
        public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        {
            return await _ctx.CarCategories.AnyAsync(c => c.Name.ToLower() == name.ToLower(), ct);
        }
    }

}
