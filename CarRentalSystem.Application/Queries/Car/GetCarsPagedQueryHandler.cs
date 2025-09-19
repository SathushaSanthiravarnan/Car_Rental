using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Car
{
    public sealed class GetCarsPagedQueryHandler : IRequestHandler<GetCarsPagedQuery, PagedResultDto<CarDto>>
    {
        private readonly ICarRepository _cars;
        private readonly IMapper _mapper;

        public GetCarsPagedQueryHandler(ICarRepository cars, IMapper mapper)
            => (_cars, _mapper) = (cars, mapper);

        public async Task<PagedResultDto<CarDto>> Handle(GetCarsPagedQuery request, CancellationToken ct)
        {
            var query = _cars.Query()
                .Include(c => c.CarModel)
                .Include(c => c.CarStatus);

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(c => c.CarModel.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<CarDto>(items, total, request.Page, request.PageSize);
        }
    }
}
