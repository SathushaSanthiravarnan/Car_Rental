using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Manufacturer
{
    public sealed class SoftDeleteManufacturerCommandHandler
     : IRequestHandler<SoftDeleteManufacturerCommand, bool>
    {
        private readonly IManufacturerRepository _manufacturers;
        private readonly IApplicationDbContext _db;

        public SoftDeleteManufacturerCommandHandler(
            IManufacturerRepository manufacturers,
            IApplicationDbContext db)
        {
            _manufacturers = manufacturers;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeleteManufacturerCommand request, CancellationToken ct)
        {
            var manufacturer = await _manufacturers.GetByIdAsync(request.Id, ct);
            if (manufacturer is null) return false;

            manufacturer.IsDeleted = true;
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
