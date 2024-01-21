using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Postify.Abstractions.Infrastructure;

namespace Postify.Infrastructure;

public class RoleBasedAuthorizationPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public RoleBasedAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
            return policy;

        return new AuthorizationPolicyBuilder()
            .RequireClaim(CustomClaims.UserRole, policyName)
            .Build();
    }
}
