using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;
using Bookshelf.Domain;

namespace Bookshelf.Application.Genres.Commands.PublishGenre;

internal sealed class PublishGenreCommandHandler : IRequestHandler<PublishGenreCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public PublishGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(PublishGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(genre => genre.Id == request.Id && !genre.Visible, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        entity.Visible = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
