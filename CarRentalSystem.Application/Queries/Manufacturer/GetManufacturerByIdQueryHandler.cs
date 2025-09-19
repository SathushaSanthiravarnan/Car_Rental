using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Manufacturer;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Manufacturer
{
    public sealed class GetManufacturerByIdQueryHandler
    : IRequestHandler<GetManufacturerByIdQuery, ManufacturerDto?>
    {
        private readonly IManufacturerRepository _manufacturers;
        private readonly IMapper _mapper;

        public GetManufacturerByIdQueryHandler(IManufacturerRepository manufacturers, IMapper mapper)
        {
            _manufacturers = manufacturers;
            _mapper = mapper;
        }

        public async Task<ManufacturerDto?> Handle(GetManufacturerByIdQuery request, CancellationToken ct)
        {
            return await _manufacturers.Query()
                .Where(m => m.Id == request.Id)
                .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
