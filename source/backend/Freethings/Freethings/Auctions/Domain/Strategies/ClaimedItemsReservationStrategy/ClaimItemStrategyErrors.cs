using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;

public static class ClaimItemStrategyErrors
{
    public static string ModuleName => "Auctions.ClaimItemStrategy";
    
    public static BusinessError CannotAutomaticallyReserveItemsUsingManualAuctionType
        => BusinessError.Create("Cannot automatically reserve items using manual auction type.");
}