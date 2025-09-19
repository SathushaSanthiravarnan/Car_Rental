using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.TestDrive
{
    public sealed class SoftDeleteTestDriveCommandValidator : AbstractValidator<SoftDeleteTestDriveCommand>
    {
        public SoftDeleteTestDriveCommandValidator()
        {
            // Rule for the Id property of the command
            RuleFor(x => x.Id)
                .NotEmpty() // Ensures the Id is not an empty GUID
                .WithMessage("Test Drive ID cannot be empty.");
        }
    }
}
