using MediatR;
using Microsoft.Extensions.Logging;
using Bookshelf.Application.Interfaces;

namespace Bookshelf.Application.Common.Behaviors;

internal class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ICurrentUserService currentUserService, ILogger<LoggingBehavior<TRequest, TResponse>> logger) =>
        (_currentUserService, _logger) = (currentUserService, logger);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        var userId = _currentUserService.UserId;
        _logger.LogInformation("Notes Request: {Name} {@UserId} {@Request}", requestName, userId, request);

        return await next();
    }
}