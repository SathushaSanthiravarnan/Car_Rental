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
    public sealed class UpdateTestDriveCommandHandler : IRequestHandler<UpdateTestDriveCommand, bool>
    {
        private readonly ITestDriveRepository _testDrives;
        private readonly IApplicationDbContext _db;

        public UpdateTestDriveCommandHandler(ITestDriveRepository testDrives, IApplicationDbContext db)
        {
            _testDrives = testDrives;
            _db = db;
        }

        public async Task<bool> Handle(UpdateTestDriveCommand request, CancellationToken ct)
        {
            var testDrive = await _testDrives.GetByIdAsync(request.Dto.Id, ct);
            if (testDrive is null)
            {
                return false;
            }

            testDrive.Status = request.Dto.Status;
            testDrive.Remarks = request.Dto.Remarks;

            if (request.Dto.StaffId.HasValue)
            {
                testDrive.ApprovedBy = request.Dto.StaffId.Value;
            }

            _testDrives.Update(testDrive);
            await _db.SaveChangesAsync(ct); // Pass the 'ct' parameter here

            return true;
        }
    }
}
