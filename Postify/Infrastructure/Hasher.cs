using Postify.Abstractions.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace Postify.Services;

public class Hasher : IHasher
{
    public string HashData(string input)
    {
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));

        return Convert.ToBase64String(hashBytes);
    }
}
