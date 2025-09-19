using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Customers
{
    public class SoftDeleteCustomerCommandValidator : AbstractValidator<SoftDeleteCustomerCommand>
    {
        public SoftDeleteCustomerCommandValidator() 
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerID is requierd.");
        }
    }
 
}
