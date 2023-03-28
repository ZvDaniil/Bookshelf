using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookshelf.Domain;

namespace Bookshelf.Persistence.EntityTypeConfigurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.Id);

        builder.HasIndex(book => book.Id)
            .IsUnique();

        builder.Property(book => book.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(book => book.Description)
            .HasMaxLength(1000);

        builder.Property(book => book.ImageUrl)
            .HasMaxLength(500);

        builder.Property(book => book.Author)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(book => book.Publisher)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(book => book.ReleaseDate)
            .IsRequired();

        builder.Property(book => book.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasMany(book => book.Genres)
            .WithMany(genre => genre.Books);

        builder.HasMany(book => book.Reviews)
            .WithOne(review => review.Book)
            .HasForeignKey(book => book.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
