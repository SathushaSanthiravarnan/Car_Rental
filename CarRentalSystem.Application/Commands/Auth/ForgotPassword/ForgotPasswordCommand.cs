using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.ForgotPassword
{
    public sealed record ForgotPasswordCommand(ForgotPasswordDto Dto) : IRequest<bool>;
}