using Microsoft.EntityFrameworkCore;

namespace Postify.Persistence;

public static class ServiceProviderExtensions
{
    public static void EnsureDatabaseCreated<TContext>(this IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        dbContext.Database.EnsureCreated();
    }
}
