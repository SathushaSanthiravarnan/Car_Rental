using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Car
{
    public sealed record GetCarsPagedQuery(int Page = 1, int PageSize = 20)
     : IRequest<PagedResultDto<CarDto>>;
}
