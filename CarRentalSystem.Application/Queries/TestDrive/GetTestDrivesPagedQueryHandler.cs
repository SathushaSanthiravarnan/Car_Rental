using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.TestDrive;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.TestDrive
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

            // Count the total number of records
            var total = await query.CountAsync(ct);

            // Fetch the paged items with includes and ordering
            var items = await query
                .Include(td => td.Customer).ThenInclude(c => c.User)
                .Include(td => td.Car)
                .Include(td => td.ApprovedByStaff).ThenInclude(s => s!.User)
                .OrderByDescending(td => td.CreatedAtUtc) // Ordering for consistent paging
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TestDriveDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<TestDriveDto>(items, total, request.Page, request.PageSize);
        }
    }
}
