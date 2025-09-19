using CarRentalSystem.Domain.Entities;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        IQueryable<RefreshToken> Query();
        Task<RefreshToken?> FindByTokenAsync(string token, CancellationToken ct = default);
        Task<bool> IsActiveAsync(string token, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}