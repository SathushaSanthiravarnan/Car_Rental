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
    public sealed class SoftDeleteCarCommandHandler : IRequestHandler<SoftDeleteCarCommand, bool>
    {
        private readonly ICarRepository _cars;
        private readonly IApplicationDbContext _db;

        public SoftDeleteCarCommandHandler(ICarRepository cars, IApplicationDbContext db)
            => (_cars, _db) = (cars, db);

        public async Task<bool> Handle(SoftDeleteCarCommand request, CancellationToken ct)
        {
            var car = await _cars.GetByIdAsync(request.Id, ct);
            if (car is null) return false;

            car.IsDeleted = true;
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
