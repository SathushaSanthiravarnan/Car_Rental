using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface ITestDriveRepository : IRepository<TestDrive>
    {
        IQueryable<TestDrive> Query();
        Task<TestDrive?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default);
    }
}
