using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public sealed class SoftDeleteCarModelCommandHandler
    : IRequestHandler<SoftDeleteCarModelCommand, bool>
    {
        private readonly ICarModelRepository _carModels;
        private readonly IApplicationDbContext _db;

        public SoftDeleteCarModelCommandHandler(
            ICarModelRepository carModels,
            IApplicationDbContext db)
        {
            _carModels = carModels;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeleteCarModelCommand request, CancellationToken ct)
        {
            var carModel = await _carModels.GetByIdAsync(request.Id, ct);
            if (carModel is null)
            {
                return false;
            }

            carModel.IsDeleted = true; // Inherited from AuditableEntity
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
