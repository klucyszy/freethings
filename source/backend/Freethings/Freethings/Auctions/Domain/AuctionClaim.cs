namespace Freethings.Auctions.Domain;

public sealed class AuctionClaim
{
    public Guid Id { get; }
    public Guid ClaimedById { get; }
    public int Quantity { get; }
    public string Comment { get; }
    public DateTimeOffset Timestamp { get; }
    public bool Reserved { get; private set; }

    public AuctionClaim(Guid claimedById, int quantity, string comment, DateTimeOffset timestamp, bool reserved)
    {
        Id = Guid.NewGuid();
        ClaimedById = claimedById;
        Quantity = quantity;
        Comment = comment;
        Timestamp = timestamp;
        Reserved = reserved;
    }

    public void SelectAsReserved()
    {
        Reserved = true;
    }
}