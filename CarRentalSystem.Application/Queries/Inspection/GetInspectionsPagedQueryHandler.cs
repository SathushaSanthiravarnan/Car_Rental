using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
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
    public sealed class GetInspectionsPagedQueryHandler
         : IRequestHandler<GetInspectionsPagedQuery, PagedResultDto<InspectionDto>>
    {
        private readonly IInspectionRepository _inspections;
        private readonly IMapper _mapper;

        public GetInspectionsPagedQueryHandler(IInspectionRepository inspections, IMapper mapper)
        {
            _inspections = inspections;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<InspectionDto>> Handle(GetInspectionsPagedQuery request, CancellationToken ct)
        {
            var query = _inspections.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(i => i.Date)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<InspectionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<InspectionDto>(items, total, request.Page, request.PageSize);
        }
    }
}
