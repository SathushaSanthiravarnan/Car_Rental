using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.TestDrive
{
    public sealed class SoftDeleteTestDriveCommandHandler : IRequestHandler<SoftDeleteTestDriveCommand, bool>
    {
        private readonly ITestDriveRepository _testDrives;
        private readonly IApplicationDbContext _db;

        public SoftDeleteTestDriveCommandHandler(ITestDriveRepository testDrives, IApplicationDbContext db)
        {
            _testDrives = testDrives;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeleteTestDriveCommand request, CancellationToken ct)
        {
            // Get the entity from the database
            var testDrive = await _testDrives.GetByIdAsync(request.Id, ct);

            // If the entity is not found, return false
            if (testDrive == null)
            {
                return false;
            }

            // Mark the entity for removal. The DbContext will handle the soft delete.
            _testDrives.Remove(testDrive);

            // Save the changes to the database
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
