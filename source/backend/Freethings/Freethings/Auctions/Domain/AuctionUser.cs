namespace Freethings.Auctions.Domain;

public sealed class AuctionUser
{
    public Guid AppUserId { get; private set; }
    
    private AuctionUser() {}
    
    public AuctionUser(Guid appUserId)
    {
        AppUserId = appUserId;
    }
}