using AutoMapper;
using CarRentalSystem.Application.Commands.Manufacturer;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Manufacturer
{
    public sealed class CreateManufacturerCommandHandler
        : IRequestHandler<CreateManufacturerCommand, ManufacturerDto>
    {
        private readonly IManufacturerRepository _manufacturers;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateManufacturerCommandHandler(
            IManufacturerRepository manufacturers,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _manufacturers = manufacturers;
            _db = db;
            _mapper = mapper;
        }

        public async Task<ManufacturerDto> Handle(CreateManufacturerCommand request, CancellationToken ct)
        {
            // ✅ Case-insensitive check using EF.Functions.Like
            var manufacturerExists = await _manufacturers.Query()
                .AnyAsync(m => EF.Functions.Like(m.Name, request.Dto.Name), ct);

            if (manufacturerExists)
            {
                throw new InvalidOperationException("A manufacturer with this name already exists.");
            }

            var manufacturer = _mapper.Map<CarRentalSystem.Domain.Entities.Manufacturer>(request.Dto);

            await _manufacturers.AddAsync(manufacturer, ct);
            await _db.SaveChangesAsync(ct);


            return _mapper.Map<ManufacturerDto>(manufacturer);
        }
    }
}
