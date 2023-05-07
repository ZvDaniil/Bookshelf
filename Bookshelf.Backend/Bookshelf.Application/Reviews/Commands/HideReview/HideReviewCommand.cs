using MediatR;

namespace Bookshelf.Application.Reviews.Commands.HideReview;

public record HideReviewCommand(Guid Id) : IRequest;
