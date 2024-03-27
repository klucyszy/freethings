namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public sealed class AlwaysAllowReservationStrategy : IClaimedItemsReservationStrategy
{
    
    public bool CanReserve()
    {
        return true;
    }
}