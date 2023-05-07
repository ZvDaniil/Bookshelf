using MediatR;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Domain;
using Bookshelf.Application.Interfaces;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Application.Reviews.Commands.CreateReview;

internal sealed class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBookshelfDbContext _dbContext;

    public CreateReviewCommandHandler(ICurrentUserService currentUserService, IBookshelfDbContext dbContext) =>
        (_currentUserService, _dbContext) = (currentUserService, dbContext);

    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .IgnoreAutoIncludes()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(book => book.Id == request.BookId, cancellationToken);

        if (book is null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var review = new Review
        {
            Id = Guid.NewGuid(),
            Book = book,
            BookId = request.BookId,
            Rating = request.Rating,
            Content = request.Content,
            UserId = _currentUserService.UserId,
            UserName = _currentUserService.UserName,
            Visible = false
        };

        await _dbContext.Reviews.AddAsync(review, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}
