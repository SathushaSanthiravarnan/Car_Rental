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

    public sealed class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarDto>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _cars;
        private readonly IApplicationDbContext _db;

        public CreateCarCommandHandler(ICarRepository cars, IApplicationDbContext db, IMapper mapper)
        {
            _cars = cars;
            _db = db;
            _mapper = mapper;
        }

        public async Task<CarDto> Handle(CreateCarCommand request, CancellationToken ct)
        {
            var car = _mapper.Map<CarRentalSystem.Domain.Entities.Car>(request.Dto);
            await _cars.AddAsync(car, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarDto>(car);
        }
    }
}
