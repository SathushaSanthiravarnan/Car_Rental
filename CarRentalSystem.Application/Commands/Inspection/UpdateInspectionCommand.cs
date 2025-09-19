using CarRentalSystem.Application.DTOs.Inspection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Inspection
{
    public sealed record UpdateInspectionCommand(Guid InspectionId, UpdateInspectionDto Dto) : IRequest<InspectionDto>;
}
