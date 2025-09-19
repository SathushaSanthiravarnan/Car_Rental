using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarStatus;
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
    public sealed class GetCarStatusByIdQueryHandler : IRequestHandler<GetCarStatusByIdQuery, CarStatusDto?>
    {
        private readonly ICarStatusRepository _carStatuses;
        private readonly IMapper _mapper;

        public GetCarStatusByIdQueryHandler(ICarStatusRepository carStatuses, IMapper mapper)
            => (_carStatuses, _mapper) = (carStatuses, mapper);

        public async Task<CarStatusDto?> Handle(GetCarStatusByIdQuery request, CancellationToken ct)
        {
            return await _carStatuses.Query()
                .Where(s => s.Id == request.Id)
                .ProjectTo<CarStatusDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
