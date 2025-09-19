using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IStaffInviteRepository : IRepository<StaffInvite>
    {
        IQueryable<StaffInvite> Query();
        Task<StaffInvite?> FindByTokenAsync(string token, CancellationToken ct = default);
        Task<StaffInvite?> FindByEmailAsync(string email, CancellationToken ct = default);
    }
}
