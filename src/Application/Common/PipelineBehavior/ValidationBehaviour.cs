using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Common.Wrappers;
using Application.Interfaces.Services;
using FluentValidation;
using MediatR;

namespace Application.Common.PipelineBehavior;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ILocalizedRequest 
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IStringLocalizer _stringLocalizer;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer stringLocalizer)
    {
        _validators = validators;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var errors = validationResults
            .SelectMany(r => r.Errors)
            .Select(error =>
            {
                var localizedValue = _stringLocalizer.Get(error.ErrorMessage, request.Language);
                return new ValidationError(error.PropertyName, localizedValue);
            })
            .ToList();

        if (errors.Any())
            throw new ValidationErrorException(errors);
        
        var response = await next();

        var operationResult = response as OperationResult;
        
        if (operationResult!.ValidationErrors?.ErrorKeys.Any() == true)
        {
            operationResult.ValidationErrors.Messages = operationResult.ValidationErrors.ErrorKeys
                .Select(error => _stringLocalizer.Get(error, request.Language))
                .ToList();
        }

        return (operationResult as TResponse)!;
    }
}