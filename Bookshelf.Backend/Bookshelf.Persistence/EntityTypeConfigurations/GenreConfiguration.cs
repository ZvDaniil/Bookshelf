using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookshelf.Domain;

namespace Bookshelf.Persistence.EntityTypeConfigurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasIndex(g => g.Name)
            .IsUnique();

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(g => g.Books)
            .WithMany(b => b.Genres);

        builder.HasQueryFilter(genre => genre.Visible);
    }
}
