using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Application.Genres.Commands.CreateGenre;

internal sealed class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Guid>
{
    private readonly IBookshelfDbContext _dbContext;

    public CreateGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Books = new List<Book>()
        };

        await _dbContext.Genres.AddAsync(genre, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return genre.Id;
    }
}
