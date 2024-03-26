using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Shared.Abstractions.Messaging;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent;
}