using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Inspection
{
    public sealed class SoftDeleteInspectionCommandHandler : IRequestHandler<SoftDeleteInspectionCommand, bool>
    {
        private readonly IInspectionRepository _inspections;
        private readonly IApplicationDbContext _db;

        public SoftDeleteInspectionCommandHandler(IInspectionRepository inspections, IApplicationDbContext db)
        {
            _inspections = inspections;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeleteInspectionCommand request, CancellationToken ct)
        {
            var inspection = await _inspections.GetByIdAsync(request.InspectionId, ct);
            if (inspection is null)
            {
                return false;
            }

            inspection.IsDeleted = true;
            _inspections.Update(inspection);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
