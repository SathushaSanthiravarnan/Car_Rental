using CarRentalSystem.Application.DTOs.Inspection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Inspection
{
    public sealed record GetInspectionByIdQuery(Guid InspectionId) : IRequest<InspectionDto?>;
}
