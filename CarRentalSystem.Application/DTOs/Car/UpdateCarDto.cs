using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Car
{
    public sealed record UpdateCarDto
    {
        public Guid CarModelId { get; init; }
        public Guid CarStatusId { get; init; }
        public string PlateNumber { get; init; } = default!;
        public string Color { get; init; } = string.Empty;
        public int Mileage { get; init; }
    }
}
