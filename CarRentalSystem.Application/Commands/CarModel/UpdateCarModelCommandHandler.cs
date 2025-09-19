using AutoMapper;
using CarRentalSystem.Application.DTOs.CarModelDto;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public sealed class UpdateCarModelCommandHandler
     : IRequestHandler<UpdateCarModelCommand, CarModelDto>
    {
        private readonly ICarModelRepository _carModels;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateCarModelCommandHandler(
            ICarModelRepository carModels,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _carModels = carModels;
            _db = db;
            _mapper = mapper;
        }

        public async Task<CarModelDto> Handle(UpdateCarModelCommand request, CancellationToken ct)
        {
            var carModel = await _carModels.GetByIdAsync(request.Id, ct)
                           ?? throw new KeyNotFoundException("Car model not found.");

            // Update the properties from the DTO
            carModel.Name = request.Dto.Name;
            carModel.YearIntroduced = request.Dto.YearIntroduced;
            carModel.BodyType = request.Dto.BodyType;
            carModel.FuelType = request.Dto.FuelType;
            carModel.Transmission = request.Dto.Transmission;

            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarModelDto>(carModel);
        }
    }
}
