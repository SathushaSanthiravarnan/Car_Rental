namespace CarRentalSystem.Application.Interfaces.Services
{
    public interface IGoogleTokenValidator
    {
        Task<GooglePayload> ValidateAsync(string idToken, string expectedClientId, CancellationToken ct = default);
    }

    public sealed record GooglePayload(
        string Subject,
        string Email,
        string? GivenName,
        string? FamilyName);
}