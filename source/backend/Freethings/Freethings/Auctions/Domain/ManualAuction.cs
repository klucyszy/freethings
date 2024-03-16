using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;

namespace Freethings.Auctions.Domain;

public sealed class ManualAuction : Auction
{
    internal ManualAuction(List<AuctionClaim> auctionClaims, int availableQuantity)
        : base(auctionClaims, availableQuantity)
    {
    }
    
    public AuctionEvent.ItemsClaimed Claim(ClaimCommand command)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            throw AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Exception;
        }
        
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            command.Quantity,
            command.Comment,
            DateTimeOffset.Now,
            false
        );
        
        _auctionClaims.Add(claim);

        return new AuctionEvent.ItemsClaimed(
            claim.ClaimedById,
            claim.Quantity,
            claim.Timestamp);
    }

    public AuctionEvent.ItemsReserved Reserve(ReserveCommand command)
    {
        AuctionClaim claim = _auctionClaims.FirstOrDefault(x => x.ClaimedById == command.ClaimedById);
        
        if (claim is null)
        {
            throw AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Exception;
        }
        
        if (_availableQuantity < claim.Quantity)
        {
            throw AuctionExceptions.AvailableQuantitySmallerThanAvailable.Exception;
        }
        
        claim.SelectAsReserved();
        
        return new AuctionEvent.ItemsReserved(
            claim.ClaimedById);
    }
    
    // TODO: Think, does quantity can be decreased by handover?. Does quantity can be higher than in handover?
    public AuctionEvent.ItemsHandedOver HandOver(HandOverCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.Reserved && x.ClaimedById == command.ClaimedById);
        
        if (claim is null)
        {
            throw AuctionExceptions.CannotHandOverIfThereIsNoClaimReferenced.Exception;
        }
        
        if (_availableQuantity < claim.Quantity)
        {
            throw AuctionExceptions.AvailableQuantitySmallerThanClaimed.Exception;
        }
        
        _availableQuantity -= claim.Quantity;
        
        // TODO: What to do, if quantity is 0?
        
        return new AuctionEvent.ItemsHandedOver(
            command.ClaimedById,
            claim.Quantity,
            _availableQuantity);
    }
    
    public sealed record ClaimCommand(
        Guid ClaimedById,
        int Quantity,
        string Comment = default);

    public sealed record ReserveCommand(
        Guid ClaimedById);
    
    public sealed record HandOverCommand(
        Guid ClaimedById);
}