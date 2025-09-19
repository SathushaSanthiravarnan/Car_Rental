using CarRentalSystem.Application.DTOs.Manufacturer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Manufacturer
{
    public sealed record UpdateManufacturerCommand(Guid Id, UpdateManufacturerDto Dto) : IRequest<ManufacturerDto>;
}
