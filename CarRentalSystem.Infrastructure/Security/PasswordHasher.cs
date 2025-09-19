using CarRentalSystem.Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Security
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        public (string Hash, string Salt) Hash(string password)
        {
            // Generate a random salt
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var salt = Convert.ToBase64String(saltBytes);

            // Hash password + salt
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
            var hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public bool Verify(string password, string hash, string salt)
        {
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
            var computed = Convert.ToBase64String(hashBytes);
            return computed == hash;
        }
    }
}
