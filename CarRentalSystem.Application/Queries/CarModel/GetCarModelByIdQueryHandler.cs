using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarModelDto;
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
    public sealed class GetCarModelByIdQueryHandler
    : IRequestHandler<GetCarModelByIdQuery, CarModelDto?>
    {
        private readonly ICarModelRepository _carModels;
        private readonly IMapper _mapper;

        public GetCarModelByIdQueryHandler(ICarModelRepository carModels, IMapper mapper)
        {
            _carModels = carModels;
            _mapper = mapper;
        }

        public async Task<CarModelDto?> Handle(GetCarModelByIdQuery request, CancellationToken ct)
        {
            return await _carModels.Query()
                .Where(cm => cm.Id == request.Id)
                .ProjectTo<CarModelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
