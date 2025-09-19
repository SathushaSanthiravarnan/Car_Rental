using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Inspection;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Inspection
{
    public sealed class GetInspectionByIdQueryHandler : IRequestHandler<GetInspectionByIdQuery, InspectionDto?>
    {
        private readonly IInspectionRepository _inspections;
        private readonly IMapper _mapper;

        public GetInspectionByIdQueryHandler(IInspectionRepository inspections, IMapper mapper)
        {
            _inspections = inspections;
            _mapper = mapper;
        }

        public async Task<InspectionDto?> Handle(GetInspectionByIdQuery request, CancellationToken ct)
        {
            return await _inspections.Query()
                .Where(i => i.Id == request.InspectionId)
                .ProjectTo<InspectionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
