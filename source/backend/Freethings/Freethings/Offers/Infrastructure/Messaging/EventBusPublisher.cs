using Freethings.Shared.Messaging;
using MediatR;

namespace Freethings.Offers.Infrastructure.Messaging;

internal sealed class EventBusPublisher : IEventBus
{
    private readonly IPublisher _publisher;

    public EventBusPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IEvent
    {
        await _publisher.Publish(@event, cancellationToken);
    }
}