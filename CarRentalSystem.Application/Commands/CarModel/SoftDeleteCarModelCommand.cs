using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarModel
{
    public sealed record SoftDeleteCarModelCommand(Guid Id) : IRequest<bool>;
}
