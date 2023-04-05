using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookshelf.Domain;

namespace Bookshelf.Persistence.EntityTypeConfigurations;

internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(review => review.Id);

        builder
            .HasIndex(review => review.Id)
            .IsUnique();

        builder
            .HasOne(review => review.Book)
            .WithMany(book => book.Reviews)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(review => review.User)
            .WithMany(user => user.Reviews)
            .HasForeignKey(review => review.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
