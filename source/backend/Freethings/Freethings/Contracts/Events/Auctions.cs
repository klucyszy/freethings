using Freethings.Shared.Messaging;

namespace Freethings.Contracts.Events;

public abstract record AuctionEvent : IEvent
{
    public sealed record ItemsClaimed(
        Guid ClaimedById,
        int ClaimedQuantity,
        int AvailableQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsReserved(
        Guid ClaimedById,
        int ClaimedQuantity,
        int AvailableQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsHandedOver(
        Guid HandedOverById,
        int HandedOverQuantity,
        int AvailableQuantity) : AuctionEvent;
}