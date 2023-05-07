using MediatR;

namespace Bookshelf.Application.Reviews.Commands.CreateReview;

public record CreateReviewCommand(Guid BookId, int Rating, string Content) : IRequest<Guid>;