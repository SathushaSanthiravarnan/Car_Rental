using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarModelDto;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarModel
{

    public sealed class GetCarModelsPagedQueryHandler
    : IRequestHandler<GetCarModelsPagedQuery, PagedResultDto<CarModelDto>>
    {
        private readonly ICarModelRepository _carModels;
        private readonly IMapper _mapper;

        public GetCarModelsPagedQueryHandler(ICarModelRepository carModels, IMapper mapper)
        {
            _carModels = carModels;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<CarModelDto>> Handle(GetCarModelsPagedQuery request, CancellationToken ct)
        {
            var query = _carModels.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(cm => cm.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CarModelDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<CarModelDto>(items, total, request.Page, request.PageSize);
        }
    }
}
