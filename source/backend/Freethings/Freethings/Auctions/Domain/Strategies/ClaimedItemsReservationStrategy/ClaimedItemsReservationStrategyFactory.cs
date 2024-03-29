namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

internal static class ClaimedItemsReservationStrategyFactory
{
    public static IClaimedItemsReservationStrategy Create(AuctionType auctionType)
    {
        return auctionType switch
        {
            AuctionType.Manual => new ManualAuctionTypeReservationStrategy(),
            _ => new AlwaysAllowReservationStrategy()
        };
    }
}