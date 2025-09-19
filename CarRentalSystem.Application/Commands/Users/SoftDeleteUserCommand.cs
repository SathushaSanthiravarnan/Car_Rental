using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Users
{
    public sealed record SoftDeleteUserCommand(Guid UserId) : IRequest<bool>;
}
