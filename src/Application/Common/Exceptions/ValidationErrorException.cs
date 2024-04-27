using Application.Common.Models;

namespace Application.Common.Exceptions;

public class ValidationErrorException : Exception
{
    public ValidationErrorException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }
    
    public IEnumerable<ValidationError> Errors { get; set; }
}

public class ValidationError
{
    public ValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}