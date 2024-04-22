using Freethings.Shared.Abstractions.Domain;

namespace Freethings.PublicApi.Events.Auctions;

public abstract record AuctionAdvertEvent : IDomainEvent
{
    public sealed record MetadataChanged(
        Guid AuctionId,
        string Title,
        string Description) : AuctionAdvertEvent;
}