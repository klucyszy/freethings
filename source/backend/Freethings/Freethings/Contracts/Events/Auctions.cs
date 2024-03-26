using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Contracts.Events;

public abstract record AuctionEvent : IDomainEvent
{
    public sealed record ItemsClaimed(
        Guid ClaimedById,
        int ClaimedQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsReserved(
        Guid ClaimedById,
        int ClaimedQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsHandedOver(
        Guid HandedOverById,
        int HandedOverQuantity,
        int AvailableQuantity) : AuctionEvent;
}