using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Inspection
{
    public class CreateInspectionCommandValidator : AbstractValidator<CreateInspectionCommand>
    {
        public CreateInspectionCommandValidator()
        {
            RuleFor(x => x.Dto.CarId).NotEmpty().WithMessage("Car ID is required.");
            RuleFor(x => x.Dto.StaffId).NotEmpty().WithMessage("Staff ID is required.");
            RuleFor(x => x.Dto.InspectionType).IsInEnum().WithMessage("Invalid inspection type.");
            RuleFor(x => x.Dto.Notes).MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");
            RuleFor(x => x.Dto.Date).NotEmpty().WithMessage("Date is required.");
        }
    }
}
