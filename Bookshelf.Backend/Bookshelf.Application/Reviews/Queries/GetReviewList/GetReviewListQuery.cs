using MediatR;
using Bookshelf.Application.Reviews.Models;

namespace Bookshelf.Application.Reviews.Queries.GetReviewList;

public record GetReviewListQuery(Guid BookId) : IRequest<ReviewListVm>;