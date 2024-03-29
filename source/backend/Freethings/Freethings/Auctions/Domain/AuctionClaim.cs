namespace Freethings.Auctions.Domain;

public sealed class AuctionClaim
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public Guid ClaimedById { get; private set; }
    public Quantity Quantity { get; private set;}
    public string Comment { get; private set;}
    public DateTimeOffset? Timestamp { get; private set;}
    public bool IsReserved { get; private set; }
    public AuctionAdvert Auction { get; private set; } // EF relation

    public AuctionClaim(Guid claimedById, Guid auctionId, Quantity quantity, string comment,
        DateTimeOffset? timestamp, bool isReserved)
    {
        AuctionId = auctionId;
        ClaimedById = claimedById;
        Quantity = quantity;
        Comment = comment;
        Timestamp = timestamp;
        IsReserved = isReserved;
    }

    public void MarkAsReserved()
    {
        IsReserved = true;
    }
}