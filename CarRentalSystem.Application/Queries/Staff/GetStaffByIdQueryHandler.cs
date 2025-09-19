using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public sealed class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDto?>
    {
        private readonly IStaffRepository _staff;
        private readonly IMapper _mapper;

        public GetStaffByIdQueryHandler(IStaffRepository staff, IMapper mapper)
        { _staff = staff; _mapper = mapper; }

        public async Task<StaffDto?> Handle(GetStaffByIdQuery request, CancellationToken ct)
        {
            return await _staff.Query()
                .Where(s => s.Id == request.StaffId)
                .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
