using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.CarStatus
{
    public sealed record CreateCarStatusDto
    {
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public bool IsAvailableForRental { get; init; }
    }
}
