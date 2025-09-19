using CarRentalSystem.Application.DTOs.Manufacturer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Manufacturer
{
    public sealed record GetManufacturerByIdQuery(Guid Id) : IRequest<ManufacturerDto?>;
}
