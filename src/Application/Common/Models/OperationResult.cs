namespace Application.Common.Models;

public class OperationResult
{
    public OperationResult(ResultCode resultCode, ValidationResult? validationErrors)
    {
        ResultCode = resultCode;
        ValidationErrors = validationErrors;
    }

    public static OperationResult Ok() => new(ResultCode.Ok, null);
    public static OperationResult BadRequest(string error) => new(ResultCode.BadRequest, 
        new ValidationResult(error));

    public ResultCode ResultCode { get; protected set; }
    public ValidationResult? ValidationErrors { get; protected set; }
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; }

    public OperationResult(ResultCode resultCode, T? data, ValidationResult? validationErrors = null) 
        : base(resultCode, validationErrors)
    {
        Data = data;
    }

    public static OperationResult<T> Ok(T data) => new(ResultCode.Ok, data);
    
    public static implicit operator OperationResult<T>(ValidationResult validationResult) => 
        new(ResultCode.InternalError, default, validationResult);
}