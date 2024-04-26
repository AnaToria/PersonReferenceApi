namespace Application.Common.Models;

public class OperationResult<T>
{
    public ResultCode ResultCode { get; set; }
    public ValidationResult? ValidationErrors { get; set; }
    public T? Data { get; set; }

    public OperationResult(ResultCode resultCode, T? data, ValidationResult? validationErrors = null)
    {
        ResultCode = resultCode;
        ValidationErrors = validationErrors;
        Data = data;
    }
}