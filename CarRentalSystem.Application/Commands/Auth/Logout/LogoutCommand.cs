using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.Logout
{
    public sealed record LogoutCommand(LogoutDto Dto) : IRequest<bool>;
}