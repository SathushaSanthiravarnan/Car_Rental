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
    /// Request to fetch a paged list of Users.
    /// </summary>
    public sealed record GetUsersPagedQuery(int Page = 1, int PageSize = 20)
    : IRequest<PagedResultDto<UserDto>>;
}
