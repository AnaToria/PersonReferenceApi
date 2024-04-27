using Application.Common.Models;
using FluentValidation;

namespace Api.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionHandlingMiddleware> _logger;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next, ILogger<ValidationExceptionHandlingMiddleware> logger)
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
        catch (ValidationException exception)
        {
            var errorMessages = exception.Errors
                .Where(failure => failure is not null)
                .Select(failure => failure.ErrorMessage.Replace("'", ""))
                .ToList();

            var validationResult =
                new OperationResult<ValidationResult>(ResultCode.BadRequest, null, new ValidationResult(errorMessages));
            await context.Response.WriteAsJsonAsync(validationResult);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var response = new OperationResult<object>(ResultCode.InternalError, null);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}