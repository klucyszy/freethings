using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Contracts.Events;

public abstract record AuctionEvent : IDomainEvent
{
    public sealed record ItemsClaimed(
        Guid AuctionId,
        Guid ClaimedById,
        int ClaimedQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsReserved(
        Guid AuctionId,
        Guid ReservedById,
        int ClaimedQuantity,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsHandedOver(
        Guid AuctionId,
        Guid HandedOverById,
        int HandedOverQuantity,
        int AvailableQuantity) : AuctionEvent;
}