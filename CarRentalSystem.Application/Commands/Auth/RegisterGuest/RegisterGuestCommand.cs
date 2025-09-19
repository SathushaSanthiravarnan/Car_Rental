using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.RegisterGuest
{
    public sealed record RegisterGuestCommand(RegisterGuestDto Dto) : IRequest<Guid>;
}