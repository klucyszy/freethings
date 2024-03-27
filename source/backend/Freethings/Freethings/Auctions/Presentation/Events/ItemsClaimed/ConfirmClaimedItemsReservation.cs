using Freethings.Auctions.Application.Commands;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Messaging;
using MediatR;

namespace Freethings.Auctions.Presentation.Events.ItemsClaimed;

internal sealed class ConfirmClaimedItemsReservation : IEventConsumer<AuctionEvent.ItemsClaimed>
{
    private readonly ISender _sender;
    
    public ConfirmClaimedItemsReservation(ISender sender)
    {
        _sender = sender;
    }
    
    public async Task Handle(AuctionEvent.ItemsClaimed notification, CancellationToken cancellationToken)
    {
        await _sender.Send(new ReserveClaimedItemsCommand(
            notification.AuctionId,
            notification.ClaimedById,
            notification.ClaimedQuantity,
            false),
            cancellationToken);
    }
}