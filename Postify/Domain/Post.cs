using Postify.Abstractions;

namespace Postify.Domain;

public class Post : BaseDomainEntity<Guid>
{
    public string Subject { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public User? User { get; set; }

    public Guid UserId { get; set; }

    public bool IsActive { get; set; } = true;
}
