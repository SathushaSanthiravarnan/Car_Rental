using CarRentalSystem.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class UpdateStaffCommandValidator : AbstractValidator<UpdateStaffCommand>
    {
        public UpdateStaffCommandValidator()
        {
            RuleFor(x => x.StaffId).NotEmpty();
            RuleFor(x => x.Dto.EmailForWork).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Dto.EmailForWork));
            RuleFor(x => x.Dto.Phone).MaximumLength(25).When(x => !string.IsNullOrWhiteSpace(x.Dto.Phone));
        }
    }
}
