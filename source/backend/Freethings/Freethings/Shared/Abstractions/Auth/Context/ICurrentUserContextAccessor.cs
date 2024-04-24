namespace Freethings.Shared.Abstractions.Auth.Context;

public interface ICurrentUserContextAccessor
{
    ICurrentUser CurrentUser { get; }
}