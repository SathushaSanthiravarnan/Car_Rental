using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.CarCategory
{
    public sealed record CreateCarCategoryDto
    {
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public decimal DailyRateModifier { get; init; }
    }


}
