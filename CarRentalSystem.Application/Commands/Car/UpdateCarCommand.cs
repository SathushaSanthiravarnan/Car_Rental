using CarRentalSystem.Application.DTOs.Car;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Car
{
    public sealed record UpdateCarCommand(Guid Id, UpdateCarDto Dto) : IRequest<CarDto>;
}
