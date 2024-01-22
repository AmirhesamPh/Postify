using Microsoft.EntityFrameworkCore;
using Postify.Abstractions;
using Postify.Abstractions.Infrastructure;
using Postify.Domain;

namespace Postify.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDateConverter dateConverter,
    IHasher hasher) : DbContext(options)
{
    internal const string DefaultConnectionStringName = "Default";

    public DbSet<User> Users => Set<User>();

    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Password = hasher.HashData("admin"),
            UserRole = UserRole.Admin,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow,
            PersianCreatedDate = dateConverter.ToPersianDateTime(DateTime.UtcNow),
            PersianLastModifiedDate = dateConverter.ToPersianDateTime(DateTime.UtcNow)
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity<Guid>>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;

                entry.Entity.PersianCreatedDate = dateConverter.ToPersianDateTime(entry.Entity.CreatedDate);
            }

            entry.Entity.LastModifiedDate = DateTime.UtcNow;

            entry.Entity.PersianLastModifiedDate = dateConverter.ToPersianDateTime(entry.Entity.LastModifiedDate);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
