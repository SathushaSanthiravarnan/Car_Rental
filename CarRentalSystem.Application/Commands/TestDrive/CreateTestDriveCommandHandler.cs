using AutoMapper;
using CarRentalSystem.Application.DTOs.TestDrive;
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
    public sealed class CreateTestDriveCommandHandler : IRequestHandler<CreateTestDriveCommand, TestDriveDto>
    {
        private readonly IMapper _mapper;
        private readonly ITestDriveRepository _testDrives;
        private readonly IApplicationDbContext _db;

        public CreateTestDriveCommandHandler(IMapper mapper, ITestDriveRepository testDrives, IApplicationDbContext db)
        {
            _mapper = mapper;
            _testDrives = testDrives;
            _db = db;
        }

        public async Task<TestDriveDto> Handle(CreateTestDriveCommand request, CancellationToken ct)
        {
            var testDrive = _mapper.Map<CarRentalSystem.Domain.Entities.TestDrive>(request.Dto);
            await _testDrives.AddAsync(testDrive, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<TestDriveDto>(testDrive);
        }
    }
}
