namespace Freethings.Shared.Abstractions.Domain.BusinessOperations;

public sealed record BusinessError
{
    public string Module { get; init; }
    public string Message { get; init; }

    private BusinessError(string message, string module = null)
    {
        Message = message;
        Module = module;
    }
    
    public static BusinessError Create(string message, string module = null)
    {
        return new BusinessError(message, module);
    }
}