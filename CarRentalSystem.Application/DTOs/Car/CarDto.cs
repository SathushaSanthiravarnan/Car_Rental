using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Car
{
    public sealed record CarDto
    {
        public Guid Id { get; init; }
        public Guid CarModelId { get; init; }
        public string CarModelName { get; init; } = default!;
        public string CarStatusName { get; init; } = default!;
        public string PlateNumber { get; init; } = default!;
        public string Color { get; init; } = string.Empty;
        public int Mileage { get; init; }
    }
}
