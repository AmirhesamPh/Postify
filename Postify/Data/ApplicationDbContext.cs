using Microsoft.EntityFrameworkCore;
using Postify.Abstractions;
using Postify.Domain;
using System.Reflection;

namespace Postify.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IDateConverter _dateConverter;
    private readonly IHasher _hasher;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateConverter dateConverter,
        IHasher hasher)
        : base(options)
    {
        _dateConverter = dateConverter;
        _hasher = hasher;
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Password = _hasher.HashData("123"),
            UserRole = UserRole.Admin
        });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity<Guid>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;

                entry.Entity.PersianCreatedDate = _dateConverter.ToPersianDateTime(entry.Entity.CreatedDate);
            }

            entry.Entity.LastModifiedDate = DateTime.UtcNow;

            entry.Entity.PersianLastModifiedDate = _dateConverter.ToPersianDateTime(entry.Entity.LastModifiedDate);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

public static class InitialDatabaseCreation
{
    public static void EnsureDatabaseCreated(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureCreated();
    }
}
