using Postify.Abstractions.Infrastructure;

namespace Postify.Infrastructure;

public static class HttpContextExtensions
{
    public static Guid GetAuthenticatedUserId(this HttpContext httpContext)
    {
        var userId = httpContext.User.Claims
            .SingleOrDefault(c => c.Type == CustomClaims.UserId)?.Value;

        if (!Guid.TryParse(userId, out var result))
            return Guid.Empty;

        return result;
    }
}
