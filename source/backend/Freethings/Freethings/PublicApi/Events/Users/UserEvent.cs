using Freethings.Shared.Abstractions.Domain;

namespace Freethings.PublicApi.Events.Users;

public abstract record UserEvent : IDomainEvent
{
    public sealed record UserCreated(
        Guid UserId,
        DateTimeOffset CreatedAt) : UserEvent;
}