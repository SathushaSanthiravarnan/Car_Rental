using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Common;
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
    /// Handles fetching paged results of Users.
    /// </summary>
    public sealed class GetUsersPagedQueryHandler
    : IRequestHandler<GetUsersPagedQuery, PagedResultDto<UserDto>>
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public GetUsersPagedQueryHandler(IUserRepository users, IMapper mapper)
            => (_users, _mapper) = (users, mapper);

        public async Task<PagedResultDto<UserDto>> Handle(GetUsersPagedQuery request, CancellationToken ct)
        {
            var query = _users.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(u => u.FirstName).ThenBy(u => u.LastName)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<UserDto>(items, total, request.Page, request.PageSize);
        }
    }
}
