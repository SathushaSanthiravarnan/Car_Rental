using CarRentalSystem.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
    {
        public CreateStaffCommandValidator()
        {
            RuleFor(x => x.Dto.UserId).NotEmpty();
            RuleFor(x => x.Dto.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Dto.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Dto.StaffType).NotEmpty();
            RuleFor(x => x.Dto.EmailForWork).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Dto.EmailForWork));
            RuleFor(x => x.Dto.Phone).MaximumLength(25).When(x => !string.IsNullOrWhiteSpace(x.Dto.Phone));
        }
    }
}
