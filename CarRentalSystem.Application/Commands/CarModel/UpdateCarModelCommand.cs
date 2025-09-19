using CarRentalSystem.Application.DTOs.CarModelDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public sealed record UpdateCarModelCommand(Guid Id, UpdateCarModelDto Dto) : IRequest<CarModelDto>;
}
