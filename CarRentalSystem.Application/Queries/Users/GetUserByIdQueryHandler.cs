using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.User;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Users
{
    /// <summary>
    /// Handles fetching a single user by Id.
    /// </summary>
    public sealed class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository users, IMapper mapper)
            => (_users, _mapper) = (users, mapper);

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            return await _users.Query()
                .Where(u => u.Id == request.UserId)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
