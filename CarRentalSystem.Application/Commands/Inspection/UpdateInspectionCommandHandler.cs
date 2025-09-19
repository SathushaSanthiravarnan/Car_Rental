using AutoMapper;
using CarRentalSystem.Application.DTOs.Inspection;
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
    public sealed class UpdateInspectionCommandHandler : IRequestHandler<UpdateInspectionCommand, InspectionDto>
    {
        private readonly IInspectionRepository _inspections;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateInspectionCommandHandler(IInspectionRepository inspections, IApplicationDbContext db, IMapper mapper)
        {
            _inspections = inspections;
            _db = db;
            _mapper = mapper;
        }

        public async Task<InspectionDto> Handle(UpdateInspectionCommand request, CancellationToken ct)
        {
            var inspection = await _inspections.GetByIdAsync(request.InspectionId, ct)
                ?? throw new KeyNotFoundException($"Inspection with ID {request.InspectionId} not found.");

            _mapper.Map(request.Dto, inspection);
            _inspections.Update(inspection);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<InspectionDto>(inspection);
        }
    }
}
