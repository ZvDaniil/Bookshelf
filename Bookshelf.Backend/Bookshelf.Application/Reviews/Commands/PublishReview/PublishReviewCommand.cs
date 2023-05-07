using MediatR;

namespace Bookshelf.Application.Reviews.Commands.PublishReview;

public record PublishReviewCommand(Guid Id) : IRequest;
