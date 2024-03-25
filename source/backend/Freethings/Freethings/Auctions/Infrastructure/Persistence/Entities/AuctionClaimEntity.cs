namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionClaimEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Quantity { get; set; }
    public string Comment { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public bool IsReserved { get; set; }
    public bool IsRejected { get; set; }
}