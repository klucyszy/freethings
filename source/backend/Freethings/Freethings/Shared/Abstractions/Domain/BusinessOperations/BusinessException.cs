namespace Freethings.Shared.Abstractions.Domain.BusinessOperations;

public sealed class BusinessException : Exception
{
    public BusinessException(string errorDescription) : base(errorDescription)
    {
    }
}