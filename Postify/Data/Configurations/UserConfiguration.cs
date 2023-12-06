using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Abstractions;
using Postify.Domain;

namespace Postify.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly IHasher _hasher;

    public UserConfiguration(IHasher hasher)
    {
        _hasher = hasher;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Username)
            .IsRequired();

        builder
            .HasIndex(x => x.Username)
            .IsUnique();

        builder
            .HasMany(x => x.UserPosts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
