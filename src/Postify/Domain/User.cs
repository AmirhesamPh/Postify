using Postify.Abstractions;

namespace Postify.Domain;

public class User : BaseDomainEntity<Guid>
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public UserRole UserRole { get; set; }

    public ICollection<Post>? UserPosts { get; set; }
}
