using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Inspection
{
    public class UpdateInspectionCommandValidator : AbstractValidator<UpdateInspectionCommand>
    {
        public UpdateInspectionCommandValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().WithMessage("Inspection ID is required.");
            RuleFor(x => x.Dto.InspectionType).IsInEnum().WithMessage("Invalid inspection type.").When(x => x.Dto.InspectionType.HasValue);
            RuleFor(x => x.Dto.Notes).MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.").When(x => !string.IsNullOrWhiteSpace(x.Dto.Notes));
        }
    }
}
