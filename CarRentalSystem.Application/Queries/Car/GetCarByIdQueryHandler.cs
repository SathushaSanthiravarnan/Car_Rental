using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Car;
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
    public sealed class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto?>
    {
        private readonly ICarRepository _cars;
        private readonly IMapper _mapper;

        public GetCarByIdQueryHandler(ICarRepository cars, IMapper mapper)
            => (_cars, _mapper) = (cars, mapper);

        public async Task<CarDto?> Handle(GetCarByIdQuery request, CancellationToken ct)
        {
            return await _cars.Query()
                .Where(c => c.Id == request.Id)
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
