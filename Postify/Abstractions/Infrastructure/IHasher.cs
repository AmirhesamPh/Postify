namespace Postify.Abstractions.Infrastructure;

public interface IHasher
{
    string HashData(string input);
}
