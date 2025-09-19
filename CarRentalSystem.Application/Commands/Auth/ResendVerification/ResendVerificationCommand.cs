using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.ResendVerification
{
    public sealed record ResendVerificationCommand(ResendVerificationDto Dto) : IRequest<bool>;
}