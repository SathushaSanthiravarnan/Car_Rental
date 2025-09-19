using CarRentalSystem.Application.DTOs.CarModelDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarModel
{
    public sealed record GetCarModelByIdQuery(Guid Id) : IRequest<CarModelDto?>;
}
