namespace Freethings.Auctions.Domain;

public sealed class AuctionClaim
{
    public Guid Id { get; }
    public Guid ClaimedById { get; }
    public int Quantity { get; }
    public string Comment { get; }
    public DateTimeOffset? Timestamp { get; }
    public bool IsReserved { get; private set; }

    public AuctionClaim(Guid claimedById, int quantity, string comment, DateTimeOffset? timestamp, bool isReserved)
    {
        Id = Guid.NewGuid();
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