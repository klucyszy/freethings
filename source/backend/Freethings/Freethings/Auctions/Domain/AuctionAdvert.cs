using Freethings.PublicApi.Events.Auctions;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;
using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Domain;

public sealed class AuctionAdvert : AggregateRoot
{
    public Guid UserId { get; private set; }
    public AuctionState State { get; private set; } = AuctionState.Draft;
    public AuctionType Type { get; private set; }
    public Quantity Quantity { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public DateTimeOffset LastModifiedAt { get; private set; }

    public List<AuctionClaim> Claims { get; private set; } = [];

    private AuctionAdvert()
    {
    } // for EF Core

    public AuctionAdvert(
        Guid userId,
        Quantity quantity,
        AuctionType type,
        Title title,
        Description description,
        DateTimeOffset timestamp)
    {
        UserId = userId;
        Quantity = quantity;
        Type = type;
        Title = title;
        Description = description;
        LastModifiedAt = timestamp;
    }

    public void UpdateState(AuctionAggregate aggregate)
    {
        Quantity = aggregate.AvailableQuantity;
        Claims.Update(
            aggregate.Claims.ToList(),
            (ent, aggr) => ent.ClaimedById == aggr.ClaimedById,
            auctionClaim => new AuctionClaim(
                auctionClaim.ClaimedById,
                Id,
                auctionClaim.Quantity,
                auctionClaim.Comment,
                auctionClaim.Timestamp,
                auctionClaim.IsReserved
            ));
        _domainEvents = aggregate.DomainEvents.ToList();
    }

    public BusinessResult Publish(DateTimeOffset timestamp)
    {
        State = AuctionState.Published;

        AddDomainEvent(new AuctionEvent.AdvertPublished(
            Id,
            UserId,
            timestamp
        ));

        return BusinessResult.Success();
    }
    
    public void ChangeAuctionMetadata(Title title, Description description)
    {
        Title = title;
        Description = description;
        
        AddDomainEvent(new AuctionAdvertEvent.MetadataChanged(
            Id,
            Title.Value,
            Description.Value));
    }
}