using AutoMapper;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, StaffDto>
    {
        private readonly IStaffRepository _staff;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateStaffCommandHandler(IStaffRepository staff, IApplicationDbContext db, IMapper mapper)
        { _staff = staff; _db = db; _mapper = mapper; }

        public async Task<StaffDto> Handle(UpdateStaffCommand request, CancellationToken ct)
        {
            var entity = await _staff.Query().Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == request.StaffId, ct)
                ?? throw new InvalidOperationException("Staff not found.");

            _mapper.Map(request.Dto, entity);

            if (request.Dto.IsActive.HasValue) entity.IsActive = request.Dto.IsActive.Value;

            _staff.Update(entity);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StaffDto>(entity);
        }
    }
}
