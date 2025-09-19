using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarStatus
{
    public sealed class SoftDeleteCarStatusCommandHandler : IRequestHandler<SoftDeleteCarStatusCommand, bool>
    {
        private readonly ICarStatusRepository _carStatuses;
        private readonly IApplicationDbContext _db;

        public SoftDeleteCarStatusCommandHandler(ICarStatusRepository carStatuses, IApplicationDbContext db)
            => (_carStatuses, _db) = (carStatuses, db);

        public async Task<bool> Handle(SoftDeleteCarStatusCommand request, CancellationToken ct)
        {
            var carStatus = await _carStatuses.GetByIdAsync(request.Id, ct);
            if (carStatus is null) return false;

            carStatus.IsDeleted = true;
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
