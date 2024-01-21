using Postify.Domain;

namespace Postify.Responses
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }

    public static partial class DomainDtoExtensions
    {
        public static PostDto ToDto(this Post post)
            => new()
            {
                Id = post.Id,
                Subject = post.Subject,
                Content = post.Content
            };
    }
}
