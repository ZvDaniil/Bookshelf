using MediatR;

namespace Bookshelf.Application.Reviews.Commands.UpdateReview;

public record UpdateReviewCommand(Guid Id, int Rating, string Content) : IRequest;
