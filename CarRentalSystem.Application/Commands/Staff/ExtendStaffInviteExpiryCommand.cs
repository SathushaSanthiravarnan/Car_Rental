using CarRentalSystem.Application.DTOs.Staff;
using MediatR;

namespace CarRentalSystem.Application.Commands.Staff
{
    /// <summary>
    /// Command to extend the expiry date of a staff invite.
    /// </summary>
    public sealed record ExtendStaffInviteExpiryCommand(
        Guid InviteId,
        DateTime NewExpiryUtc,
        string RequestedBy
    ) : IRequest<StaffInviteDto>;
}