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
    public sealed class UpdateCarStatusCommandHandler : IRequestHandler<UpdateCarStatusCommand, CarStatusDto>
    {
        private readonly ICarStatusRepository _carStatuses;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateCarStatusCommandHandler(ICarStatusRepository carStatuses, IApplicationDbContext db, IMapper mapper)
            => (_carStatuses, _db, _mapper) = (carStatuses, db, mapper);

        public async Task<CarStatusDto> Handle(UpdateCarStatusCommand request, CancellationToken ct)
        {
            var carStatus = await _carStatuses.GetByIdAsync(request.Id, ct)
                ?? throw new KeyNotFoundException("Car status not found.");

            _mapper.Map(request.Dto, carStatus);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarStatusDto>(carStatus);
        }
    }
}
