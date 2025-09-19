using AutoMapper;
using CarRentalSystem.Application.DTOs.CarStatus;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarStatus
{
    public sealed class CreateCarStatusCommandHandler : IRequestHandler<CreateCarStatusCommand, CarStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ICarStatusRepository _carStatuses;
        private readonly IApplicationDbContext _db;

        public CreateCarStatusCommandHandler(ICarStatusRepository carStatuses, IApplicationDbContext db, IMapper mapper)
        {
            _carStatuses = carStatuses;
            _db = db;
            _mapper = mapper;
        }

        public async Task<CarStatusDto> Handle(CreateCarStatusCommand request, CancellationToken ct)
        {
            var carStatus = _mapper.Map<CarRentalSystem.Domain.Entities.CarStatus>(request.Dto);
            await _carStatuses.AddAsync(carStatus, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarStatusDto>(carStatus);
        }
    }
}
