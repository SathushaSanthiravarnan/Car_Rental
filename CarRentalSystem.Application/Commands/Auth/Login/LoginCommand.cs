using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.Login
{
    public sealed record LoginCommand(LoginDto Dto, string? Ip = null, string? UserAgent = null)
        : IRequest<AuthResultDto>;
}