using Freethings.Auctions.Domain.Exceptions;
using Freethings.Auctions.Domain.Strategies;
using Freethings.Contracts.Events;
using Freethings.Shared.Domain;
using Freethings.Shared.Domain.Exceptions;

namespace Freethings.Auctions.Domain;

public sealed class Auction : AggregateRoot
{
    private readonly IClaimBehaviorStrategy _claimBehaviorStrategy;
    private readonly List<AuctionClaim> _auctionClaims;
    
    private int _availableQuantity;
    
    public enum AuctionType
    {
        Manual,
        FirstComeFirstServed
    }
    
    public  Auction(List<AuctionClaim> auctionClaims, int availableQuantity, AuctionType auctionType)
    {
        _auctionClaims = auctionClaims;
        _availableQuantity = availableQuantity;
        _claimBehaviorStrategy = ClaimBehaviorStrategyFactory.Create(auctionType, auctionClaims, availableQuantity);
    }
    
    public void Claim(ClaimCommand command)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            throw AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Exception;
        }
        
        ClaimStrategyResult<AuctionClaim, DomainException> result =
            _claimBehaviorStrategy.Claim(command);
        
        if (!result.CanBeClaimed)
        {
            throw result.FailureReason;
        }
        
        _auctionClaims.Add(result.Claim);

        if (result.Claim.Reserved)
        {
            _availableQuantity -= result.Claim.Quantity;
            
            AddDomainEvent(new AuctionEvent.ItemsReserved(
                result.Claim.ClaimedById,
                result.Claim.Quantity,
                _availableQuantity,
                result.Claim.Timestamp));
        }
        else
        {
            AddDomainEvent(new AuctionEvent.ItemsClaimed(
                result.Claim.ClaimedById,
                result.Claim.Quantity,
                _availableQuantity,
                result.Claim.Timestamp));
        }
    }

    public AuctionEvent.ItemsReserved Reserve(ReserveCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.ClaimedById == command.ClaimedById);
        
        if (claim is null)
        {
            throw AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Exception;
        }
        
        if (_availableQuantity < claim.Quantity)
        {
            throw AuctionExceptions.AvailableQuantitySmallerThanAvailable.Exception;
        }
        
        claim.MarkAsReserved();
        
        return new AuctionEvent.ItemsReserved(
            claim.ClaimedById,
            claim.Quantity,
            _availableQuantity,
            DateTimeOffset.Now);
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