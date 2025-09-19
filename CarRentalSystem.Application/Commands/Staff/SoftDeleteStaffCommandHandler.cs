using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class SoftDeleteStaffCommandHandler : IRequestHandler<SoftDeleteStaffCommand, bool>
    {
        private readonly IStaffRepository _staff;
        private readonly IApplicationDbContext _db;

        public SoftDeleteStaffCommandHandler(IStaffRepository staff, IApplicationDbContext db)
        { _staff = staff; _db = db; }

        public async Task<bool> Handle(SoftDeleteStaffCommand request, CancellationToken ct)
        {
            var entity = await _staff.GetByIdAsync(request.StaffId, ct);
            if (entity is null) return false;

            entity.IsDeleted = true;
            _staff.Update(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }

}
