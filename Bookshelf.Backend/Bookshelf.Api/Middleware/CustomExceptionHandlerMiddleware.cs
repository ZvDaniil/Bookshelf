using System.Net;
using System.Text.Json;
using FluentValidation;
using Bookshelf.Application.Common.Exceptions;

namespace Bookshelf.Api.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (code, result) = exception switch
        {
            ValidationException validationException => (HttpStatusCode.BadRequest, JsonSerializer.Serialize(validationException.Errors)),
            NotFoundException => (HttpStatusCode.NotFound, string.Empty),
            _ => (HttpStatusCode.InternalServerError, string.Empty)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        result = string.IsNullOrEmpty(result)
            ? JsonSerializer.Serialize(new { errpr = exception.Message })
            : result;

        return context.Response.WriteAsync(result);
    }
}
