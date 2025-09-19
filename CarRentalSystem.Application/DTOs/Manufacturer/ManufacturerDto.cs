using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Manufacturer
{
    public sealed record ManufacturerDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Country { get; init; }
        public int? FoundedYear { get; init; }
    }
}
