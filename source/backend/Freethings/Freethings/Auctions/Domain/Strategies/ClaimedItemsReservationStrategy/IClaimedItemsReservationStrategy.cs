using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public interface IClaimedItemsReservationStrategy
{
    public BusinessResult CanReserve();
}