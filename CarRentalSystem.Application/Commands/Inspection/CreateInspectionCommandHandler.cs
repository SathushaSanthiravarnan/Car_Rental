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
    public sealed class CreateInspectionCommandHandler : IRequestHandler<CreateInspectionCommand, InspectionDto>
    {
        private readonly IInspectionRepository _inspections;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateInspectionCommandHandler(IInspectionRepository inspections, IApplicationDbContext db, IMapper mapper)
        {
            _inspections = inspections;
            _db = db;
            _mapper = mapper;
        }

        public async Task<InspectionDto> Handle(CreateInspectionCommand request, CancellationToken ct)
        {
            var inspection = _mapper.Map<CarRentalSystem.Domain.Entities.Inspection>(request.Dto);

            await _inspections.AddAsync(inspection, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<InspectionDto>(inspection);
        }
    }
}

