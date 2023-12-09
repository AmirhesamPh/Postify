using Postify.Domain;

namespace Postify.Abstractions.Persistence;

public interface IPostRepository
{
    IQueryable<Post> GetAllByUserId(Guid userId);

    Task<Post?> GetByIdAsync(Guid id);

    Task<Post> AddAsync(
        string subject,
        string content,
        Guid userId);

    Task UpdateAsync(Post post);

    Task DeleteAsync(Post post);

    Task SetPostStatusAsync(Post post, bool status);
}
