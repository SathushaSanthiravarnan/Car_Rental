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
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ApplicationDbContext _ctx;

        public UserRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<User> Query() => _ctx.Users.AsQueryable();

        public async Task<User?> FindByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), ct);
        }

        public async Task<User?> FindByUsernameAsync(string username, CancellationToken ct = default)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(u => u.Username != null &&
                                          u.Username.ToLower() == username.ToLower(), ct);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _ctx.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower(), ct);
        }
    }
}
