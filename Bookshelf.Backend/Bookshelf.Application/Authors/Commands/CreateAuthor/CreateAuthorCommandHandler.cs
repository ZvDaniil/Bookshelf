using MediatR;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Application.Authors.Commands.CreateAuthor;

internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IBookshelfDbContext _dbContext;

    public CreateAuthorCommandHandler(IBookshelfDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Books = new List<Book>(),
            Visible = request.Visible
        };

        await _dbContext.Authors.AddAsync(author, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
