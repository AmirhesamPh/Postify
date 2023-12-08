using Microsoft.EntityFrameworkCore;
using Postify.Abstractions.Persistence;
using Postify.Persistence.Repositories;

namespace Postify.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString(ApplicationDbContext.DefaultConnectionStringName);

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}
