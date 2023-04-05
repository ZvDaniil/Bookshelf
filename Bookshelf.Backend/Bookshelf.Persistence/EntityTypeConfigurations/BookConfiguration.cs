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
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(book => book.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(book => book.AgeRestriction)
            .IsRequired();

        builder.Property(book => book.DatePublished)
            .IsRequired();

        builder.Property(book => book.Pages)
            .IsRequired();

        builder.Property(book => book.Price)
           .HasColumnType("decimal(18,2)")
           .IsRequired();

        builder.Property(book => book.ISBN)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(book => book.Author)
            .WithMany(author => author.Books);

        builder.HasMany(book => book.Genres)
            .WithMany(genre => genre.Books);

        builder.HasMany(book => book.Reviews)
            .WithOne(review => review.Book)
            .HasForeignKey(r => r.BookId);
    }
}