using CarRentalSystem.Application.DTOs.Staff;
using MediatR;
using System;

namespace CarRentalSystem.Application.Commands.Staff
{
    /// <summary>
    /// Command to resend a staff invite email.
    /// </summary>
    public sealed record ResendStaffInviteCommand(
        Guid InviteId,
        string Email,
        string RequestedBy
    ) : IRequest<bool>; // return true if resent successfully
}