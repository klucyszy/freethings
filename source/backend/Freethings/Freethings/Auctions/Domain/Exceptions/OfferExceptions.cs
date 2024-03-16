using Freethings.Shared.Exceptions;

namespace Freethings.Auctions.Domain.Exceptions;

public static class AuctionExceptions
{
    public static class SameUserCannotCreateTwoClaimsOnOneAuction
    { 
        public static string Message => "Same user cannot create two claims on one auction";
        public static DomainException Exception => new (Message);
    }
    
    public static class CannotReserveItemsIfThereIsNoClaimReferenced
    { 
        public static string Message => "Cannot reserve items if there is no claim referenced";
        public static DomainException Exception => new (Message);
    }
}