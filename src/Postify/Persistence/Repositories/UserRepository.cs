using Microsoft.EntityFrameworkCore;
using Postify.Abstractions.Persistence;
using Postify.Domain;

namespace Postify.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUsernameRegisteredAsync(string username)
        => await _dbContext.Users.AnyAsync(u => u.Username == username);

    public async Task<User?> GetByUsernameAsync(string username)
        => await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);

    public async Task<User> AddAsync(string username, string passwordHash, UserRole userRole)
    {
        var addedUser = await _dbContext.Users.AddAsync(new()
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = passwordHash,
            UserRole = userRole
        });

        await _dbContext.SaveChangesAsync();

        return addedUser.Entity;
    }
}
