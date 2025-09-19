using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarStatus;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarStatus
{
    public sealed class GetCarStatusesPagedQueryHandler : IRequestHandler<GetCarStatusesPagedQuery, PagedResultDto<CarStatusDto>>
    {
        private readonly ICarStatusRepository _carStatuses;
        private readonly IMapper _mapper;

        public GetCarStatusesPagedQueryHandler(ICarStatusRepository carStatuses, IMapper mapper)
            => (_carStatuses, _mapper) = (carStatuses, mapper);

        public async Task<PagedResultDto<CarStatusDto>> Handle(GetCarStatusesPagedQuery request, CancellationToken ct)
        {
            var query = _carStatuses.Query();
            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(s => s.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CarStatusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<CarStatusDto>(items, total, request.Page, request.PageSize);
        }
    }
}
