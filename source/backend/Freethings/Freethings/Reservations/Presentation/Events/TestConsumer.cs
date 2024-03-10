using Freethings.Contracts;
using Freethings.Shared.Messaging;

namespace Freethings.Reservations.Presentation.Events;

public sealed class TestConsumer : IEventConsumer<OfferPublished>
{
    public async Task Handle(OfferPublished notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken);
        
        Console.WriteLine("OfferPublished consumer handled the event.");
    }
}