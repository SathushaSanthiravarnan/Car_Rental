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
    internal sealed class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CustomerRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Customer> Query() => _ctx.Customers.AsQueryable();

        public async Task<Customer?> FindByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _ctx.Customers.FirstOrDefaultAsync(c => c.UserId == userId, ct);
        }
    }
}
