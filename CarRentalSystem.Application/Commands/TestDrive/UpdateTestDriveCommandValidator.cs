using CarRentalSystem.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.TestDrive
{
    public sealed class UpdateTestDriveCommandValidator : AbstractValidator<UpdateTestDriveCommand>
    {
        public UpdateTestDriveCommandValidator()
        {
            RuleFor(x => x.Dto.Id)
                .NotEmpty().WithMessage("Test Drive ID is required for update.");

            RuleFor(x => x.Dto.Status)
                .IsInEnum().WithMessage("Invalid test drive status.");

            RuleFor(x => x.Dto.StaffId)
                .NotEmpty().When(x => x.Dto.Status is TestDriveStatus.Approved or TestDriveStatus.Rejected)
                .WithMessage("Staff ID is required to approve or reject a test drive.");
        }
    }
}
