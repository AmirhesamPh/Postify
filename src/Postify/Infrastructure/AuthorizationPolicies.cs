using Postify.Domain;

namespace Postify.Abstractions.Infrastructure;

public static class AuthorizationPolicies
{
    public static readonly string AdminUser = UserRole.Admin.ToString();
}
