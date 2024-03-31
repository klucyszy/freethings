using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public sealed class ManualAuctionTypeReservationStrategy : IClaimedItemsReservationStrategy
{
    public BusinessResult CanReserve()
    {
        return ClaimItemStrategyErrors
            .CannotAutomaticallyReserveItemsUsingManualAuctionType
            .AsBusinessResult();
    }
}