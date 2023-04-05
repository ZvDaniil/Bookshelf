using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Genres.Commands.DeleteGenre;

internal sealed class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public DeleteGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        _dbContext.Genres.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
