using Postify.Abstractions.Persistence;
using Postify.Domain;

namespace Postify.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Post> GetAllByUserId(Guid userId)
        => _dbContext.Posts.Where(p => p.UserId == userId);

    public async Task<Post?> GetByIdAsync(Guid id)
        => await _dbContext.Posts.FindAsync(id);

    public async Task<Post> AddAsync(string subject, string content, Guid userId)
    {
        var addedPost = await _dbContext.Posts.AddAsync(new()
        {
            Id = Guid.NewGuid(),
            Subject = subject,
            Content = content,
            UserId = userId
        });

        await _dbContext.SaveChangesAsync();

        return addedPost.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        _dbContext.Posts.Update(post);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _dbContext.Posts.Remove(post);

        await _dbContext.SaveChangesAsync();
    }

    public async Task TogglePostStatusAsync(Post post, bool status)
    {
        post.IsActive = status;

        _dbContext.Posts.Update(post);

        await _dbContext.SaveChangesAsync();
    }
}
