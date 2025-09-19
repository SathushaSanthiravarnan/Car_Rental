using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.GoogleSignIn
{
    public sealed record GoogleSignInCommand(GoogleSignInDto Dto) : IRequest<AuthResultDto>;
}