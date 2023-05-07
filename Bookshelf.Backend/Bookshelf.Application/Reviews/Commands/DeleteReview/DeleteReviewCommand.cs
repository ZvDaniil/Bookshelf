using MediatR;

namespace Bookshelf.Application.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(Guid Id) : IRequest;
