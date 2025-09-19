using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Common
{
    public sealed record PagedRequestDto(
        int Page = 1,
        int PageSize = 20,
        string? Search = null
    );
}
