namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionClaimEntity
{
    public Guid Id { get; set; }
    public Guid AuctionId { get; set; }
    public Guid UserId { get; set; }
    public int Quantity { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public bool IsReserved { get; set; }
    public bool IsRejected { get; set; }
    
    public AuctionEntity Auction { get; private set; }

    private AuctionClaimEntity() {}
    
    public AuctionClaimEntity(Guid userId, int quantity, string comment, DateTimeOffset? timestamp,
        bool isReserved)
    {
        UserId = userId;
        Quantity = quantity;
        Comment = comment;
        Timestamp = timestamp;
        IsReserved = isReserved;
    }
}