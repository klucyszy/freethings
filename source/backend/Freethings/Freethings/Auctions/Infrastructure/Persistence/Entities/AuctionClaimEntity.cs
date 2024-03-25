namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionClaimEntity
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public Guid UserId { get; private set; }
    public int Quantity { get; private set; }
    public string Comment { get; private set; }
    public DateTimeOffset? Timestamp { get; private set; }
    public bool IsReserved { get; private set; }
    public bool IsRejected { get; private set; }
    
    public AuctionEntity Auction { get; private set; }

    public AuctionClaimEntity() {}
    
    
}