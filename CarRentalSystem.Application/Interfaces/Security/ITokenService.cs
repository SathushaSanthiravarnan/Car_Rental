using CarRentalSystem.Domain.Entities;

namespace CarRentalSystem.Application.Interfaces.Security
{
    public interface ITokenService
    {
        (string token, DateTime expiresAt) CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}