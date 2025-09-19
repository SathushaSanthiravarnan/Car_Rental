using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Common
{
    public sealed record ApiResponseDto<T>(
        bool Success,
        T? Data,
        string? Message = null
    );
}
