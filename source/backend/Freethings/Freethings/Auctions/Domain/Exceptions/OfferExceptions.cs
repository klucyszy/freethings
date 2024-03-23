using Freethings.Shared.Abstractions.Domain.Exceptions;

namespace Freethings.Auctions.Domain.Exceptions;

public static class AuctionExceptions
{
    public static class SameUserCannotCreateTwoClaimsOnOneAuction
    { 
        public static string Message => "Same user cannot create two claims on one auction";
        public static DomainException Exception => new (Message);
    }
    
    public static class CannotReserveIfThereIsNoClaimReferenced
    { 
        public static string Message => "Cannot reserve items if there is no claim referenced";
        public static DomainException Exception => new (Message);
    }
    
    public static class AvailableQuantitySmallerThanAvailable
    { 
        public static string Message => "The claimed quantity is greater than the available quantity that can be reserved";
        public static DomainException Exception => new (Message);
    }
    
    public static class CannotHandOverIfThereIsNoClaimReferenced
    { 
        public static string Message => "Cannot hand over items if there is no reserved claim referenced";
        public static DomainException Exception => new (Message);
    }
    
    public static class AvailableQuantitySmallerThanClaimed
    { 
        public static string Message => "The claimed quantity is greater than the available quantity";
        public static DomainException Exception => new (Message);
    }
}