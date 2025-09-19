using AutoMapper;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategory
{
    public sealed class UpdateCarCategoryCommandHandler : IRequestHandler<UpdateCarCategoryCommand, CarCategoryDto>
    {
        private readonly ICarCategoryRepository _categories;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateCarCategoryCommandHandler(ICarCategoryRepository categories, IApplicationDbContext db, IMapper mapper)
            => (_categories, _db, _mapper) = (categories, db, mapper);

        public async Task<CarCategoryDto> Handle(UpdateCarCategoryCommand request, CancellationToken ct)
        {
            var category = await _categories.GetByIdAsync(request.Id, ct)
                ?? throw new KeyNotFoundException("Car category not found.");

            _mapper.Map(request.Dto, category);

            await _db.SaveChangesAsync(ct);

            return _mapper.Map<CarCategoryDto>(category);
        }
    }
}
