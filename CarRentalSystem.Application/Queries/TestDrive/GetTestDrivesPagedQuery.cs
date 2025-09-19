using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.TestDrive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.TestDrive
{
    public sealed record GetTestDrivesPagedQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResultDto<TestDriveDto>>;
}
