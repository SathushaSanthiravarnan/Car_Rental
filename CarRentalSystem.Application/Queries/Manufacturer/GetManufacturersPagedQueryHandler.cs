using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Manufacturer;
using CarRentalSystem.Application.DTOs.TestDrive;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Queries.TestDrive;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Manufacturer
{
    public sealed class GetTestDrivesPagedQueryHandler : IRequestHandler<GetTestDrivesPagedQuery, PagedResultDto<TestDriveDto>>
    {
        private readonly ITestDriveRepository _testDrives;
        private readonly IMapper _mapper;

        public GetTestDrivesPagedQueryHandler(ITestDriveRepository testDrives, IMapper mapper)
        {
            _testDrives = testDrives;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<TestDriveDto>> Handle(GetTestDrivesPagedQuery request, CancellationToken ct)
        {
            var query = _testDrives.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .Include(td => td.Customer).ThenInclude(c => c.User)
                .Include(td => td.Car)
                .Include(td => td.ApprovedByStaff).ThenInclude(s => s!.User)
                .OrderByDescending(td => td.CreatedAtUtc)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TestDriveDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<TestDriveDto>(items, total, request.Page, request.PageSize);
        }
    }
}
