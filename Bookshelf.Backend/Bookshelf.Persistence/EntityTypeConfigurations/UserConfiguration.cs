using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookshelf.Domain;

namespace Bookshelf.Persistence.EntityTypeConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName)
            .HasMaxLength(50);

        builder.Property(user => user.LastName)
           .HasMaxLength(50);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasMany(user => user.Reviews)
            .WithOne(review => review.User);
    }
}