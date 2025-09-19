using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Queryable access for LINQ queries.
        /// </summary>
        IQueryable<User> Query();

        /// <summary>
        /// Find a user by email (case-insensitive).
        /// </summary>
        Task<User?> FindByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Find a user by username (case-insensitive).
        /// </summary>
        Task<User?> FindByUsernameAsync(string username, CancellationToken ct = default);

        /// <summary>
        /// Check if email already exists.
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
    }
}
