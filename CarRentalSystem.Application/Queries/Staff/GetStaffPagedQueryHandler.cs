using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Staff
{
    public sealed class GetStaffPagedQueryHandler : IRequestHandler<GetStaffPagedQuery, PagedResultDto<StaffDto>>
    {
        private readonly IStaffRepository _staff;
        private readonly IMapper _mapper;

        public GetStaffPagedQueryHandler(IStaffRepository staff, IMapper mapper)
        { _staff = staff; _mapper = mapper; }

        public async Task<PagedResultDto<StaffDto>> Handle(GetStaffPagedQuery request, CancellationToken ct)
        {
            var q = _staff.Query().OrderByDescending(s => s.JoinedOnUtc);

            var total = await q.CountAsync(ct);
            var items = await q.Skip((request.Page - 1) * request.PageSize)
                               .Take(request.PageSize)
                               .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
                               .ToListAsync(ct);

            return new PagedResultDto<StaffDto>(items, total, request.Page, request.PageSize);
        }
    }
}
