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
    public sealed class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, StaffDto>
    {
        private readonly IStaffRepository _staff;
        private readonly IUserRepository _users;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateStaffCommandHandler(IStaffRepository staff, IUserRepository users, IApplicationDbContext db, IMapper mapper)
        {
            _staff = staff; _users = users; _db = db; _mapper = mapper;
        }

        public async Task<StaffDto> Handle(CreateStaffCommand request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.Dto.UserId, ct)
                       ?? throw new InvalidOperationException("User not found.");

            var exists = await _staff.Query().AnyAsync(s => s.UserId == user.Id, ct);
            if (exists) throw new InvalidOperationException("Staff already exists for this user.");

            var entity = _mapper.Map<Domain.Entities.Staff>(request.Dto);
            entity.UserId = user.Id;
            entity.IsActive = true;
            entity.JoinedOnUtc = DateTime.UtcNow;

            await _staff.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);

            // include user for the mapping (if not tracked)
            entity.User = user;

            return _mapper.Map<StaffDto>(entity);
        }
    }
}
