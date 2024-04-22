using Freethings.Shared.Abstractions.Domain;

namespace Freethings.PublicApi.Events.Auctions;

public abstract record AuctionEvent : IDomainEvent
{
    public sealed record AdvertCreated(
        Guid AuctionId,
        Guid CreatedById,
        string Title,
        string Description,
        DateTimeOffset Timestamp) : AuctionEvent;
    
    public sealed record AdvertPublished(
        Guid AuctionId,
        Guid PublishedById,
        DateTimeOffset Timestamp) : AuctionEvent;
    
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
    
    public sealed record ItemsReservationCancelled(
        Guid AuctionId,
        Guid ReservedById,
        DateTimeOffset Timestamp) : AuctionEvent;

    public sealed record ItemsHandedOver(
        Guid AuctionId,
        Guid HandedOverById,
        int HandedOverQuantity,
        int AvailableQuantity) : AuctionEvent;
}