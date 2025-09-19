using CarRentalSystem.Application.DTOs.Staff;
using MediatR;
using System;

namespace CarRentalSystem.Application.Commands.Staff
{
    /// <summary>
    /// Command to refresh (regenerate) a staff invite token.
    /// </summary>
    public sealed record RefreshStaffInviteTokenCommand(
        Guid InviteId,
        string RequestedBy
    ) : IRequest<StaffInviteDto>;
}