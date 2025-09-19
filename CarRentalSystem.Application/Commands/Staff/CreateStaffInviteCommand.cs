using CarRentalSystem.Application.DTOs.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Staff
{
    public sealed record CreateStaffInviteCommand(CreateStaffInviteDto Dto) : IRequest<StaffInviteDto>;
}
