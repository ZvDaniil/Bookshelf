namespace Bookshelf.Persistence;

public static class DbInitializer
{
    public static void Initialize(BookshelfDbContext context)
    {
        context.Database.EnsureCreated();
    }
}