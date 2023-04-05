using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Genres.Commands.UpdateGenre;

internal sealed class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public UpdateGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        entity.Name = request.Name;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}