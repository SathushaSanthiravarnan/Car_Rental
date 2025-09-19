using CarRentalSystem.Application.DTOs.CarCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategory
{
   
    public sealed record UpdateCarCategoryCommand(Guid Id, UpdateCarCategoryDto Dto) : IRequest<CarCategoryDto>;

}
