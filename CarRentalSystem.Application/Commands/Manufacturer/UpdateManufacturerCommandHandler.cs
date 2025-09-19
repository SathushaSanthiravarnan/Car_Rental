using AutoMapper;
using CarRentalSystem.Application.DTOs.Manufacturer;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Manufacturer
{
    public sealed class UpdateManufacturerCommandHandler
    : IRequestHandler<UpdateManufacturerCommand, ManufacturerDto>
    {
        private readonly IManufacturerRepository _manufacturers;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateManufacturerCommandHandler(
            IManufacturerRepository manufacturers,
            IApplicationDbContext db,
            IMapper mapper)
        {
            _manufacturers = manufacturers;
            _db = db;
            _mapper = mapper;
        }

        public async Task<ManufacturerDto> Handle(UpdateManufacturerCommand request, CancellationToken ct)
        {
            var manufacturer = await _manufacturers.GetByIdAsync(request.Id, ct)
                               ?? throw new KeyNotFoundException("Manufacturer not found.");

            manufacturer.Name = request.Dto.Name;
            manufacturer.Country = request.Dto.Country;
            manufacturer.FoundedYear = request.Dto.FoundedYear;

            await _db.SaveChangesAsync(ct);
            return _mapper.Map<ManufacturerDto>(manufacturer);
        }
    }
}
