namespace Freethings.Shared.Abstractions.Domain.BusinessOperations;

public sealed record BusinessResult
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    private BusinessResult(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static BusinessResult Success()
    {
        return new BusinessResult(true, null);
    }

    public static BusinessResult Failure(string errorMessage)
    {
        return new BusinessResult(false, errorMessage);
    }
}

public sealed record BusinessResult<T>
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public T Data { get; }

    private BusinessResult(bool isSuccess, string errorMessage, T data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }

    public static BusinessResult<T> Success(T value)
    {
        return new BusinessResult<T>(true, null, value);
    }

    public static BusinessResult<T> Failure(params string[] errorMessages)
    {
        string errorMessage = string.Join(", ", errorMessages);
        return new BusinessResult<T>(false, errorMessage, default);
    }
}