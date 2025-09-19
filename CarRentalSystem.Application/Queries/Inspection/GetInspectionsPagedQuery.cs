using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Inspection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Inspection
{
    public sealed record GetInspectionsPagedQuery(int Page = 1, int PageSize = 20)
       : IRequest<PagedResultDto<InspectionDto>>;
}
