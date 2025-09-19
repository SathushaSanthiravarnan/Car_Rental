using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Common
{
    public sealed record PagedResultDto<T>(
        IReadOnlyList<T> Items,
        int Page,
        int PageSize,
        int TotalCount
    );
}
