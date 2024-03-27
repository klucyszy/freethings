namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public sealed class ManualAuctionTypeReservationStrategy : IClaimedItemsReservationStrategy
{
    public bool CanReserve()
    {
        return false;
    }
}