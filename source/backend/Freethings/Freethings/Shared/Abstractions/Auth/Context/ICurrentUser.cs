namespace Freethings.Shared.Abstractions.Auth.Context;

public interface ICurrentUser
{
    public Guid? Identity { get; }
    public bool IsAuthenticated { get; }
}