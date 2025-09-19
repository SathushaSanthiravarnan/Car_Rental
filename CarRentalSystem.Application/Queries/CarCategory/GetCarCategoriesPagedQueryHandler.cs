using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.CarCategory
{
    public sealed class GetCarCategoriesPagedQueryHandler : IRequestHandler<GetCarCategoriesPagedQuery, PagedResultDto<CarCategoryDto>>
    {
        private readonly ICarCategoryRepository _categories;
        private readonly IMapper _mapper;

        public GetCarCategoriesPagedQueryHandler(ICarCategoryRepository categories, IMapper mapper)
            => (_categories, _mapper) = (categories, mapper);

        public async Task<PagedResultDto<CarCategoryDto>> Handle(GetCarCategoriesPagedQuery request, CancellationToken ct)
        {
            var query = _categories.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderBy(c => c.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CarCategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<CarCategoryDto>(items, total, request.Page, request.PageSize);
        }
    }
}
