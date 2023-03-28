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

        builder.Property(review => review.Title)
            .HasMaxLength(100);

        builder.Property(review => review.Body)
            .HasMaxLength(500);

        builder.Property(review => review.Rating)
            .IsRequired();

        builder.Property(review => review.Date)
            .IsRequired();

        builder.HasOne(review => review.Book)
            .WithMany(book => book.Reviews)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
