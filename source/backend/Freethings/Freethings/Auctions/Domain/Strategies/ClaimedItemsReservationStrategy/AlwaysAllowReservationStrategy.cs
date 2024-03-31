using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public sealed class AlwaysAllowReservationStrategy : IClaimedItemsReservationStrategy
{
    public BusinessResult CanReserve()
    {
        return BusinessResult.Success();
    }
}