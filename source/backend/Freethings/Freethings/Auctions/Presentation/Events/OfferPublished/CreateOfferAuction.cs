using Freethings.Contracts.Events;
using Freethings.Shared.Messaging;

namespace Freethings.Auctions.Presentation.Events.OfferPublished;

public sealed class CreateOfferAuction : IEventConsumer<OfferEvent.Published>
{
    public async Task Handle(OfferEvent.Published notification, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken);
        
        Console.WriteLine("OfferEvent.Published consumer handled the event.");
    }
}