namespace Freethings.Auctions.Domain;

public abstract class Auction
{
    public enum Type
    {
        Manual,
        Random,
        FirstComeFirstServed
    }
    
    protected List<AuctionClaim> _auctionClaims;
    protected int _availableQuantity;

    internal Auction(List<AuctionClaim> auctionClaims, int availableQuantity)
    {
        _auctionClaims = auctionClaims;
        _availableQuantity = availableQuantity;
    }
}

public sealed class AuctionFactory
{
    public static Auction Create(Auction.Type auctionType, List<AuctionClaim> auctionClaims, int availableQuantity)
    {
        return auctionType switch
        {
            Auction.Type.Manual => new ManualAuction(auctionClaims, availableQuantity),
            // Auction.SelectionType.Random => new RandomAuction(auctionClaims, availableQuantity),
            // Auction.SelectionType.FirstComeFirstServed => new FirstComeFirstServedAuction(auctionClaims, availableQuantity),
            _ => throw new ArgumentOutOfRangeException(nameof(auctionType), auctionType, null)
        };
    }
}