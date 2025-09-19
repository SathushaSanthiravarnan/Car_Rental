using CarRentalSystem.Application.DTOs.Auth;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.ResetPassword
{
    public sealed record ResetPasswordCommand(ResetPasswordDto Dto) : IRequest<bool>;
}