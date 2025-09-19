using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.TestDrive
{
    public sealed record SoftDeleteTestDriveCommand(Guid Id) : IRequest<bool>;
}
