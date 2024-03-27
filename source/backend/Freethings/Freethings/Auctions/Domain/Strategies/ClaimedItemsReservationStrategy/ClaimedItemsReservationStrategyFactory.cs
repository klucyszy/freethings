namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

internal static class ClaimedItemsReservationStrategyFactory
{
    public static IClaimedItemsReservationStrategy Create(Auction.AuctionType auctionType)
    {
        return auctionType switch
        {
            Auction.AuctionType.Manual => new ManualAuctionTypeReservationStrategy(),
            _ => new AlwaysAllowReservationStrategy()
        };
    }
}