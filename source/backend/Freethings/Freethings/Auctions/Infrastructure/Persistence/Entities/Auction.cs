using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

public sealed class Auction
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public AuctionMetadata Metadata { get; set; }
    public List<AuctionClaim> Claims { get; set; } = [];
}

public sealed class AuctionMetadata
{
    public Guid Id { get; set; }
    public AuctionTitle Title { get; set; }
    public AuctionDescription Description { get; set; }
}

public sealed class AuctionClaim
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Quantity { get; set; }
    public bool IsReserved { get; set; }
    public bool IsRejected { get; set; }
}