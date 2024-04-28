namespace Application.Common.Models;

public class ValidationResult
{
    public ValidationResult(string error)
    {
        ErrorKeys = new []{ error };
        IsValid = false;
    }
    
    public ValidationResult(IEnumerable<string> errors)
    {
        ErrorKeys = errors;
        IsValid = false;
    }
    
    internal IEnumerable<string> ErrorKeys { get; }
    public IEnumerable<string> Messages { get; set; }
    public bool IsValid { get; }

    public static implicit operator ValidationResult(string message) => new(message);
    public static implicit operator ValidationResult(Exception exception) => new(exception.Message);
}