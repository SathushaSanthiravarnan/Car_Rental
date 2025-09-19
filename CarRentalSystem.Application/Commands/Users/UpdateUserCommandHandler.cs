using AutoMapper;
using CarRentalSystem.Application.DTOs.User;
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
    public sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _users;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository users, IApplicationDbContext db, IMapper mapper)
            => (_users, _db, _mapper) = (users, db, mapper);

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.UserId, ct)
                       ?? throw new KeyNotFoundException("User not found.");

            // required in DTO
            user.FirstName = request.Dto.FirstName;
            user.LastName = request.Dto.LastName;

            // optional updates
            if (!string.IsNullOrWhiteSpace(request.Dto.Phone))
                user.Phone = request.Dto.Phone;

            if (!string.IsNullOrWhiteSpace(request.Dto.Username))
                user.Username = request.Dto.Username;

            if (request.Dto.Address is not null)
                user.Address = _mapper.Map(request.Dto.Address, user.Address); // upsert

            if (request.Dto.IsActive.HasValue)
                user.IsActive = request.Dto.IsActive.Value;

            await _db.SaveChangesAsync(ct);
            return _mapper.Map<UserDto>(user);
        }
    }
}
