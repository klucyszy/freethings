using Freethings.Auctions.Domain.Exceptions;
using Freethings.Auctions.Domain.Strategies;
using Freethings.Auctions.Domain.Strategies.Abstractions;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.Exceptions;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Domain;

public sealed class Auction : AggregateRoot
{
    public IReadOnlyCollection<AuctionClaim> Claims => _auctionClaims.AsReadOnly();
    public int AvailableQuantity => _availableQuantity;
    
    private readonly IClaimBehaviorStrategy _claimBehaviorStrategy;
    private readonly ICurrentTime _currentTime;
    
    private readonly List<AuctionClaim> _auctionClaims;
    private int _availableQuantity;
    
    public enum AuctionType
    {
        Manual,
        FirstComeFirstServed
    }
    
    public  Auction(List<AuctionClaim> auctionClaims, int availableQuantity, AuctionType auctionType, ICurrentTime currentTime)
    {
        _auctionClaims = auctionClaims;
        _availableQuantity = availableQuantity;
        _currentTime = currentTime;
        _claimBehaviorStrategy = ClaimBehaviorStrategyFactory.Create(auctionType, availableQuantity, currentTime);
    }
    
    public void Claim(ClaimCommand command)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            throw AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Exception;
        }
        
        if (_availableQuantity < command.Quantity)
        {
            throw AuctionExceptions.AvailableQuantitySmallerThanAvailable.Exception;
        }
        
        ClaimStrategyResult<AuctionClaim, DomainException> result =
            _claimBehaviorStrategy.Claim(command);
        
        if (!result.CanBeClaimed)
        {
            throw result.FailureReason;
        }
        
        _auctionClaims.Add(result.Claim);

        if (result.Claim.IsReserved)
        {
            AddDomainEvent(new AuctionEvent.ItemsReserved(
                result.Claim.ClaimedById,
                result.Claim.Quantity,
                result.Claim.Timestamp.Value));
        }
        else
        {
            AddDomainEvent(new AuctionEvent.ItemsClaimed(
                result.Claim.ClaimedById,
                result.Claim.Quantity,
                result.Claim.Timestamp.Value));
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
            _currentTime.Now());
    }
    
    // TODO: Think, does quantity can be decreased by handover?. Does quantity can be higher than in handover?
    public AuctionEvent.ItemsHandedOver HandOver(HandOverCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.IsReserved && x.ClaimedById == command.ClaimedById);
        
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
    
    public void ChangeAvailableQuantity(int newQuantity)
    {
        _availableQuantity = newQuantity;
        
        // there should be validated if new quantity is not less than already reserved. If so, cancel reservations
        
    }
    
    public sealed record ClaimCommand(
        Guid ClaimedById,
        int Quantity,
        string Comment = default);

    public sealed record ReserveCommand(
        Guid ClaimedById);
    
    public sealed record HandOverCommand(
        Guid ClaimedById,
        int? Quantity = null);
}