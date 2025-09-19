using CarRentalSystem.Application.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.CarCategory
{
    public class SoftDeleteCarCategoryCommandValidator : AbstractValidator<SoftDeleteCarCategoryCommand>
    {
        public SoftDeleteCarCategoryCommandValidator()
        {


            RuleFor(x => x.Id).NotEmpty().WithMessage("CustomerID is requierd.");
        }
    }
    
}
