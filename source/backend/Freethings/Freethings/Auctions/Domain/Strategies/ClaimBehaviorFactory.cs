using Freethings.Auctions.Domain.Strategies.Abstractions;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Domain.Strategies;

public static class ClaimBehaviorStrategyFactory
{
    public static IClaimBehaviorStrategy Create(Auction.AuctionType auctionType, int availableQuantity, ICurrentTime currentTime)
    {
        return auctionType switch
        {
            Auction.AuctionType.Manual => new ManualClaimBehaviorStrategy(currentTime),
            Auction.AuctionType.FirstComeFirstServed => new FirstComeFirstServedClaimBehaviorStrategy(availableQuantity, currentTime),
            _ => throw new ArgumentOutOfRangeException(nameof(auctionType), auctionType, null)
        };
    }
}