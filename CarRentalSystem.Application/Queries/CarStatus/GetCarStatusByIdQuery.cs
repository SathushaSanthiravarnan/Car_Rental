using CarRentalSystem.Application.DTOs.CarStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarStatus
{
    public sealed record GetCarStatusByIdQuery(Guid Id) : IRequest<CarStatusDto?>;
}
