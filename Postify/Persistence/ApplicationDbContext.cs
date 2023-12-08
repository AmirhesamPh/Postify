using Microsoft.EntityFrameworkCore;
using Postify.Abstractions;
using Postify.Abstractions.Infrastructure;
using Postify.Domain;

namespace Postify.Persistence;

public class ApplicationDbContext : DbContext
{
    internal const string DefaultConnectionStringName = "Default";

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Password = _hasher.HashData("admin"),
            UserRole = UserRole.Admin,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow,
            PersianCreatedDate = _dateConverter.ToPersianDateTime(DateTime.UtcNow),
            PersianLastModifiedDate = _dateConverter.ToPersianDateTime(DateTime.UtcNow)
        });
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
