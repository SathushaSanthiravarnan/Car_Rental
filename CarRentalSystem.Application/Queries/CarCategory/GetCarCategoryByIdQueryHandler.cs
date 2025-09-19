using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.CarCategory;
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
    public sealed class GetCarCategoryByIdQueryHandler : IRequestHandler<GetCarCategoryByIdQuery, CarCategoryDto?>
    {
        private readonly ICarCategoryRepository _categories;
        private readonly IMapper _mapper;

        public GetCarCategoryByIdQueryHandler(ICarCategoryRepository categories, IMapper mapper)
            => (_categories, _mapper) = (categories, mapper);

        public async Task<CarCategoryDto?> Handle(GetCarCategoryByIdQuery request, CancellationToken ct)
        {
            return await _categories.Query()
                .Where(c => c.Id == request.Id)
                .ProjectTo<CarCategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
