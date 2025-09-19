using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarCategory
{
    public sealed record GetCarCategoriesPagedQuery(int Page = 1, int PageSize = 20)
        : IRequest<PagedResultDto<CarCategoryDto>>;
}
