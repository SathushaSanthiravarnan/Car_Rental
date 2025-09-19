using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.RefreshToken
{
    public sealed record RefreshTokenCommand(RefreshTokenDto Dto) : IRequest<AuthResultDto>;
}