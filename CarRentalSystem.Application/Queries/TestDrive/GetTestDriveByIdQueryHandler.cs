using AutoMapper;
using CarRentalSystem.Application.DTOs.TestDrive;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.TestDrive
{

    public sealed class GetTestDriveByIdQueryHandler : IRequestHandler<GetTestDriveByIdQuery, TestDriveDto?>
    {
        private readonly ITestDriveRepository _testDrives;
        private readonly IMapper _mapper;

        public GetTestDriveByIdQueryHandler(ITestDriveRepository testDrives, IMapper mapper)
        {
            _testDrives = testDrives;
            _mapper = mapper;
        }

        public async Task<TestDriveDto?> Handle(GetTestDriveByIdQuery request, CancellationToken ct)
        {
            var testDrive = await _testDrives.GetByIdWithDetailsAsync(request.Id, ct);

            return _mapper.Map<TestDriveDto?>(testDrive);
        }
    }
}
