using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Shared.Infrastructure.Messaging;

internal sealed class EventBusPublisher : IEventBus
{
    private readonly IPublisher _publisher;

    public EventBusPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent
    {
        await _publisher.Publish(@event, cancellationToken);
    }
}