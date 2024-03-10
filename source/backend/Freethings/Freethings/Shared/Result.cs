namespace Freethings.Shared;

public sealed record Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    private Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(string errorMessage)
    {
        return new Result(false, errorMessage);
    }
}

public sealed record Result<T>
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public T Data { get; }

    private Result(bool isSuccess, string errorMessage, T data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, null, value);
    }

    public static Result<T> Failure(params string[] errorMessages)
    {
        string errorMessage = string.Join(", ", errorMessages);
        return new Result<T>(false, errorMessage, default);
    }
}