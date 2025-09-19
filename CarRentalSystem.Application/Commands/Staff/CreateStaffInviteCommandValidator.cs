using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed class CreateStaffInviteCommandValidator : AbstractValidator<CreateStaffInviteCommand>
    {
        public CreateStaffInviteCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
        }
    }

}
