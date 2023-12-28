using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace CompanyManagementSystem.Infrastructure.Helpers;

public static class HashHelper
{
    public static string Hash(string email, string password)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(email),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));
    }
}
