using Microsoft.AspNetCore.Authorization;
using Postify.Abstractions.Infrastructure;
using Postify.Services;
using System.Globalization;

namespace Postify.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrasrtucture(this IServiceCollection services)
    {
        services
            .AddSingleton<PersianCalendar>()
            .AddSingleton<IDateConverter, PersianDateConverter>();

        services.AddSingleton<IHasher, Hasher>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddSingleton<IAuthorizationPolicyProvider, RoleBasedAuthorizationPolicyProvider>();

        return services;
    }
}
