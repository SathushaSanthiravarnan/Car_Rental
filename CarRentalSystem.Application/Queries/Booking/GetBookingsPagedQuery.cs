using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Booking
{
    public sealed record GetBookingsPagedQuery(int Page = 1, int PageSize = 20)
     : IRequest<PagedResultDto<BookingDto>>;
}
