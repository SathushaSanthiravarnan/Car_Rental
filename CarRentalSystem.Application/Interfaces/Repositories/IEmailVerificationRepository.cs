using CarRentalSystem.Domain.Entities;

namespace CarRentalSystem.Application.Interfaces.Repositories
{
    public interface IEmailVerificationRepository : IRepository<EmailVerification>
    {
        IQueryable<EmailVerification> Query();
        Task<EmailVerification?> FindByUserAndTokenAsync(Guid userId, string token, CancellationToken ct = default);
        Task<bool> ExistsValidAsync(Guid userId, string token, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}