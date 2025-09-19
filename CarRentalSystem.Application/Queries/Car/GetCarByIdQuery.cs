using CarRentalSystem.Application.DTOs.Car;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Car
{
    public sealed record GetCarByIdQuery(Guid Id) : IRequest<CarDto?>;
}
