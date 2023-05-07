using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookshelf.Domain;

namespace Bookshelf.Persistence.EntityTypeConfigurations;

internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(review => review.Id);

        builder.HasIndex(review => review.Id)
            .IsUnique();

        builder.Property(review => review.Content)
            .HasMaxLength(2048);

        builder.HasOne(review => review.Book)
            .WithMany(book => book.Reviews)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(review => review.Visible);
    }
}