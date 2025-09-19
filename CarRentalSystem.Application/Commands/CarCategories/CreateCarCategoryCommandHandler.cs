using AutoMapper;
using CarRentalSystem.Application.Commands.CarCategory;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategories
{
    public sealed class CreateCarCategoryCommandHandler
         : IRequestHandler<CreateCarCategoryCommand, CarCategoryDto>
    {
        private readonly ICarCategoryRepository _categories;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CreateCarCategoryCommandHandler(
            ICarCategoryRepository categories,
            IApplicationDbContext db,
            IMapper mapper
        ) => (_categories, _db, _mapper) = (categories, db, mapper);

        public async Task<CarCategoryDto> Handle(CreateCarCategoryCommand request, CancellationToken ct)
        {
            var categoryExists = await _categories.Query()
                .AnyAsync(c => c.Name.ToLower() == request.Dto.Name.ToLower(), ct);

            if (categoryExists)
                throw new InvalidOperationException($"A category with the name '{request.Dto.Name}' already exists.");

            var category = _mapper.Map<CarRentalSystem.Domain.Entities.CarCategory>(request.Dto);
            await _categories.AddAsync(category, ct);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarCategoryDto>(category);
        }
    }
}
