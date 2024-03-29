using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Domain;

public sealed class AuctionAdvert : AggregateRoot
{
    public AuctionState State { get; private set; } = AuctionState.Draft;
    public AuctionType Type { get; private set; }
    public Quantity Quantity { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public List<AuctionClaim> Claims { get; private set; } = [];
    
    private AuctionAdvert() {} // for EF Core
    
    public AuctionAdvert(
        Quantity quantity,
        AuctionType type,
        Title title,
        Description description)
    {
        Quantity = quantity;
        Type = type;
        Title = title;
        Description = description;
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
                auctionClaim.IsReserved));
        _domainEvents = aggregate.DomainEvents.ToList();
    }
}