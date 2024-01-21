using Postify.Domain;

namespace Postify.Abstractions.Persistence;

public interface IUserRepository
{
    Task<bool> IsUsernameRegisteredAsync(string username);

    Task<User?> GetByUsernameAsync(string username);

    Task<User> AddAsync(
        string username,
        string passwordHash,
        UserRole userRole);
}
