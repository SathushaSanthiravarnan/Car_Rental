using CarRentalSystem.Application.DTOs.TestDrive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.TestDrive
{
    public sealed record GetTestDriveByIdQuery(Guid Id) : IRequest<TestDriveDto?>;
}
