using AutoMapper;
using CarRentalSystem.Application.DTOs.CarModelDto;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Domain.Entities;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public sealed class CreateCarModelCommandHandler
        : IRequestHandler<CreateCarModelCommand, CarModelDto>
    {
        private readonly ICarModelRepository _carModels;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateCarModelCommandHandler(
            ICarModelRepository carModels,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _carModels = carModels;
            _db = db;
            _mapper = mapper;
        }

        public async Task<CarModelDto> Handle(CreateCarModelCommand request, CancellationToken ct)
        {
            // 🔎 Validate Manufacturer exists
            var manufacturerExists = await _db.Manufacturers
                .AnyAsync(m => m.Id == request.Dto.ManufacturerId, ct);

            if (!manufacturerExists)
            {
                throw new InvalidOperationException("Invalid ManufacturerId. Manufacturer not found.");
            }

            // 🔎 Validate CarCategory exists
            var categoryExists = await _db.CarCategories
                .AnyAsync(c => c.Id == request.Dto.CategoryId, ct);

            if (!categoryExists)
            {
                throw new InvalidOperationException("Invalid CategoryId. CarCategory not found.");
            }

            // 🔎 Check duplicate CarModel (same Manufacturer + Name)
            var carModelExists = await _carModels.Query()
                .AnyAsync(cm => cm.ManufacturerId == request.Dto.ManufacturerId
                                && cm.Name.ToLower() == request.Dto.Name.ToLower(), ct);

            if (carModelExists)
            {
                throw new InvalidOperationException(
                    "A car model with the same manufacturer and name already exists.");
            }

            // ✅ Map DTO → Entity
            var carModel = _mapper.Map<CarRentalSystem.Domain.Entities.CarModel>(request.Dto);

            try
            {
                // Insert + Save
                await _carModels.AddAsync(carModel, ct);
                await _db.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex)
            {
                // 🚨 Wrap EF error with details
                throw new InvalidOperationException(
                    $"Failed to save CarModel. InnerException: {ex.InnerException?.Message}", ex);
            }

            // ✅ Map back Entity → DTO
            return _mapper.Map<CarModelDto>(carModel);
        }
    }
}