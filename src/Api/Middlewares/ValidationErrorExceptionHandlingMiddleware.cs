using Application.Common.Exceptions;
using Application.Common.Models;

namespace Api.Middlewares;

public sealed class ValidationErrorExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationErrorExceptionHandlingMiddleware> _logger;

    public ValidationErrorExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ValidationErrorExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationErrorException exception)
        {
            await context.Response.WriteAsJsonAsync(exception.Errors);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var response = new OperationResult<object>(ResultCode.InternalError, null);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}