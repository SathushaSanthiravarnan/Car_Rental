using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Users
{
    public sealed class SoftDeleteUserCommandHandler
    : IRequestHandler<SoftDeleteUserCommand, bool>
    {
        private readonly IUserRepository _users;
        private readonly IApplicationDbContext _db;

        public SoftDeleteUserCommandHandler(IUserRepository users, IApplicationDbContext db)
            => (_users, _db) = (users, db);

        public async Task<bool> Handle(SoftDeleteUserCommand request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.UserId, ct);
            if (user is null) return false;

            user.IsDeleted = true; // from AuditableEntity
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
