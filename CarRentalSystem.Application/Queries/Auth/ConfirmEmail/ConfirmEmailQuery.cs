using MediatR;

namespace CarRentalSystem.Application.Queries.Auth.ConfirmEmail
{
    public sealed record ConfirmEmailQuery(string Email, string Token) : IRequest<bool>;
}