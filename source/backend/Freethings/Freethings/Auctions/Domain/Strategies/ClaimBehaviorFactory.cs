namespace Freethings.Auctions.Domain.Strategies;

public static class ClaimBehaviorStrategyFactory
{
    public static IClaimBehaviorStrategy Create(Auction.AuctionType auctionType, List<AuctionClaim> auctionClaims,
        int availableQuantity)
    {
        return auctionType switch
        {
            Auction.AuctionType.Manual => new ManualClaimBehaviorStrategy(),
            Auction.AuctionType.FirstComeFirstServed => new FirstComeFirstServedClaimBehaviorStrategy(availableQuantity),
            _ => throw new ArgumentOutOfRangeException(nameof(auctionType), auctionType, null)
        };
    }
}