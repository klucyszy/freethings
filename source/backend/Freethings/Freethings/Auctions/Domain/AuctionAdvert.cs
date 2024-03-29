using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Auctions.Domain;

public sealed class AuctionAdvert : AggregateRoot
{
    public Quantity Quantity { get; private set; }
    public AuctionType Type { get; private set; }
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
}