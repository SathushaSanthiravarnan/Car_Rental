using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Staff
{
    public sealed record RefreshStaffInviteTokenDto(Guid InviteId, string RequestedBy);
}
