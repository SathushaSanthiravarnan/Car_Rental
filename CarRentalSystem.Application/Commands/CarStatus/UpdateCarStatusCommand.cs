using CarRentalSystem.Application.DTOs.CarStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarStatus
{
    public sealed record UpdateCarStatusCommand(Guid Id, UpdateCarStatusDto Dto) : IRequest<CarStatusDto>;
}
