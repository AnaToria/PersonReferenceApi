using Application.Common.Models;
using FluentValidation;

namespace Api.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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
                .Select(failure => failure.ErrorMessage.Replace("'",""))
                .ToList();

            var validationResult =
                new OperationResult<ValidationResult>(ResultCode.BadRequest, null, new ValidationResult(errorMessages));
            await context.Response.WriteAsJsonAsync(validationResult);
        }
    }
}