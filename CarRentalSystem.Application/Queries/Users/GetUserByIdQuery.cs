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
    /// Request to fetch a single User by Id.
    /// </summary>
    public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;
}
