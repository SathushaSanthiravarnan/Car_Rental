using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        (string Hash, string Salt) Hash(string password);
        bool Verify(string password, string hash, string salt);
    }
}
