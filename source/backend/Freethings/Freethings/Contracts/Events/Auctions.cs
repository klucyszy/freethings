using Freethings.Shared.Messaging;

namespace Freethings.Contracts.Events;

public abstract record AuctionEvent : IEvent
{
    public sealed record ItemsClaimed(
        Guid ClaimedById,
        int ClaimedQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsReserved(
        Guid ClaimedById) : AuctionEvent;

    public sealed record ItemsHandedOver : AuctionEvent;
}