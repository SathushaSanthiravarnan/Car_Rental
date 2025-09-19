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
    public sealed class SoftDeleteCarCategoryCommandHandler : IRequestHandler<SoftDeleteCarCategoryCommand, bool>
    {
        private readonly ICarCategoryRepository _categories;
        private readonly IApplicationDbContext _db;

        public SoftDeleteCarCategoryCommandHandler(ICarCategoryRepository categories, IApplicationDbContext db)
            => (_categories, _db) = (categories, db);

        public async Task<bool> Handle(SoftDeleteCarCategoryCommand request, CancellationToken ct)
        {
            var category = await _categories.GetByIdAsync(request.Id, ct);
            if (category is null) return false;

            category.IsDeleted = true;
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }

}
