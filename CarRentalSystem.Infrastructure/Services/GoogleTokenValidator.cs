using CarRentalSystem.Application.Interfaces.Services;
using Google.Apis.Auth;

namespace CarRentalSystem.Infrastructure.Services
{
    public sealed class GoogleTokenValidator : IGoogleTokenValidator
    {
        public async Task<GooglePayload> ValidateAsync(string idToken, string expectedClientId, CancellationToken ct = default)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { expectedClientId }
            };

            // correct overload: only pass idToken + settings
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            return new GooglePayload(
                Subject: payload.Subject,
                Email: payload.Email,
                GivenName: payload.GivenName,
                FamilyName: payload.FamilyName
            );
        }
    }
}