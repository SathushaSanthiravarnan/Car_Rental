using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Inspection
{
    public class SoftDeleteInspectionCommandValidator : AbstractValidator<SoftDeleteInspectionCommand>
    {
        public SoftDeleteInspectionCommandValidator()
        {
            RuleFor(x => x.InspectionId).NotEmpty().WithMessage("Inspection ID is required for soft deletion.");
        }
    }
}
