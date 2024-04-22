using Freethings.Auctions.Application.Commands;
using Freethings.PublicApi.Events.Auctions;
using Freethings.Shared.Abstractions.Messaging;

namespace Freethings.Auctions.Presentation.Events.AuctionAdvertCreated;

internal sealed class PublishAuctionAdvert : IEventConsumer<AuctionEvent.AdvertCreated>
{
    private readonly ISender _sender;

    public PublishAuctionAdvert(ISender sender)
    {
        _sender = sender;
    }

    public Task Handle(AuctionEvent.AdvertCreated notification, CancellationToken cancellationToken)
    {
        return _sender.Send(new PublishAuctionAdvertCommand(
            notification.AuctionId), cancellationToken);
    }
}