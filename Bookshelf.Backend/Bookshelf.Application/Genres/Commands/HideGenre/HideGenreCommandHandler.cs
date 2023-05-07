using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Genres.Commands.HideGenre;

internal sealed class HideGenreCommandHandler : IRequestHandler<HideGenreCommand>
{
    private readonly IBookshelfDbContext _dbContext;

    public HideGenreCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Handle(HideGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Genres
            .FirstOrDefaultAsync(genre => genre.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        entity.Visible = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
