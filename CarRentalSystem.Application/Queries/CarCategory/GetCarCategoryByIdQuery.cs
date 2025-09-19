using CarRentalSystem.Application.DTOs.CarCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarCategory
{
   
    public sealed record GetCarCategoryByIdQuery(Guid Id) : IRequest<CarCategoryDto?>;

}
