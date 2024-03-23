namespace Freethings.Shared.Abstractions.Domain.Exceptions;

public sealed class DomainException : Exception
{
    public DomainException(string errorDescription) : base(errorDescription)
    {
    }
}