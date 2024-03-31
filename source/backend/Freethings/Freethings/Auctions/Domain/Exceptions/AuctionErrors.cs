using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain.Exceptions;

public static class AuctionErrors
{
    private static string ModuleName => "Auctions";

    public static BusinessError SameUserCannotCreateTwoClaimsOnOneAuction
        => BusinessError.Create("User {0} cannot create two claims on one auction", ModuleName);

    public static BusinessError CannotReserveIfThereIsNoClaimReferenced
        => BusinessError.Create("Cannot reserve items if there is no claim referenced", ModuleName);

    public static BusinessError AvailableQuantitySmallerThanAvailable
        => BusinessError.Create("The claimed quantity {0} is greater than the available quantity {1} that can be reserved", ModuleName);

    public static BusinessError CannotHandOverIfThereIsNoClaimReferenced
        => BusinessError.Create("Cannot hand over items if there is no reserved claim referenced", ModuleName);

    public static BusinessError AvailableQuantitySmallerThanClaimed
        => BusinessError.Create("The claimed quantity is greater than the available quantity", ModuleName);

    public static BusinessError ClaimAlreadyReserved
        => BusinessError.Create("Claim is already reserved", ModuleName);
}