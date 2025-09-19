using CarRentalSystem.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Repositories
{
    internal class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _ctx;
        protected readonly DbSet<T> _set;

        public RepositoryBase(DbContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<T>();
        }

        public Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _set.FindAsync([id], ct).AsTask();

        public Task AddAsync(T entity, CancellationToken ct = default)
            => _set.AddAsync(entity, ct).AsTask();

        public void Update(T entity) => _set.Update(entity);
        public void Remove(T entity) => _set.Remove(entity);
    }
}
