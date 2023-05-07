using MediatR;
using Bookshelf.Application.Reviews.Models;

namespace Bookshelf.Application.Reviews.Queries.GetReviewDetails;

public record GetReviewDetailsQuery(Guid Id) : IRequest<ReviewDetailsVm>;