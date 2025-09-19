using AutoMapper;
using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Car
{
    public sealed class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, CarDto>
    {
        private readonly ICarRepository _cars;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler(ICarRepository cars, IApplicationDbContext db, IMapper mapper)
            => (_cars, _db, _mapper) = (cars, db, mapper);

        public async Task<CarDto> Handle(UpdateCarCommand request, CancellationToken ct)
        {
            var car = await _cars.GetByIdAsync(request.Id, ct)
                ?? throw new KeyNotFoundException("Car not found.");

            _mapper.Map(request.Dto, car);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarDto>(car);
        }
    }
}
