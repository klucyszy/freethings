using Freethings.Auctions.Domain.Exceptions;
using Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Domain.BusinessOperations;

namespace Freethings.Auctions.Domain;

public sealed class AuctionAggregate : AggregateRoot
{
    private readonly List<AuctionClaim> _auctionClaims;
    public IReadOnlyCollection<AuctionClaim> Claims => _auctionClaims.AsReadOnly();
    
    private Quantity _availableQuantity;
    public Quantity AvailableQuantity => _availableQuantity;
    
    private readonly AuctionType _auctionType;
    
    public AuctionAggregate(
        Guid id,
        List<AuctionClaim> auctionClaims,
        Quantity availableQuantity,
        AuctionType auctionType)
    {
        Id = id;
        _auctionClaims = auctionClaims;
        _availableQuantity = availableQuantity;
        _auctionType = auctionType;
    }

    public BusinessResult Claim(ClaimCommand command, DateTimeOffset currentTime)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            return AuctionErrors
                .SameUserCannotCreateTwoClaimsOnOneAuction
                .Format(command.ClaimedById)
                .AsBusinessResult();
        }
        
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            this.Id,
            command.Quantity,
            command.Comment,
            currentTime,
            false
        );

        _auctionClaims.Add(claim);

        AddDomainEvent(new AuctionEvent.ItemsClaimed(
            Id,
            claim.ClaimedById,
            claim.Quantity.Value,
            claim.Timestamp.Value));
        
        return BusinessResult.Success();
    }

    public BusinessResult Reserve(ReserveCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.ClaimedById == command.ClaimedById);

        if (claim is null)
        {
            return AuctionErrors
                .CannotReserveIfThereIsNoClaimReferenced
                .AsBusinessResult();
        }
        
        if (claim.IsReserved)
        {
            return AuctionErrors
                .ClaimAlreadyReserved
                .AsBusinessResult();
        }
        
        IClaimedItemsReservationStrategy reservationStrategy = command.TriggeredByUser
            ? new AlwaysAllowReservationStrategy()
            : ClaimedItemsReservationStrategyFactory.Create(_auctionType);
        
        BusinessResult canReserveBusinessResult = reservationStrategy.CanReserve();
        if (!canReserveBusinessResult.IsSuccess)
        {
            return canReserveBusinessResult;
        }
        
        if (_availableQuantity < claim.Quantity)
        {
            return AuctionErrors
                .AvailableQuantitySmallerThanAvailable
                .Format(claim.Quantity, _availableQuantity)
                .AsBusinessResult();
        }

        claim.Reserve();
        
        AddDomainEvent(new AuctionEvent.ItemsReserved(
            Id,
            claim.ClaimedById,
            claim.Quantity.Value,
            claim.Timestamp.Value));
        
        return BusinessResult.Success();
    }

    // TODO: Think, does quantity can be decreased by handover?. Does quantity can be higher than in handover?
    public AuctionEvent.ItemsHandedOver HandOver(HandOverCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.IsReserved && x.ClaimedById == command.ClaimedById);

        if (claim is null)
        {
            throw AuctionErrors.CannotHandOverIfThereIsNoClaimReferenced.AsBusinessException();
        }

        if (_availableQuantity < claim.Quantity)
        {
            throw AuctionErrors
                .AvailableQuantitySmallerThanClaimed
                .AsBusinessException();
        }

        _availableQuantity -= claim.Quantity;

        // TODO: What to do, if quantity is 0?

        return new AuctionEvent.ItemsHandedOver(
            Id,
            command.ClaimedById,
            claim.Quantity.Value,
            _availableQuantity.Value);
    }

    public void ChangeAvailableQuantity(Quantity newQuantity, DateTimeOffset currentTime)
    {
        if (newQuantity < _availableQuantity)
        {
            List<AuctionClaim> reservedClaims = _auctionClaims
                .Where(x => x.IsReserved)
                .OrderByDescending(x => x.Timestamp)
                .ToList();
            List<AuctionClaim> claimsToCancel = FindClaimsToBeCancelled(reservedClaims, newQuantity);
            foreach (AuctionClaim claim in claimsToCancel)
            {
                claim.CancelReservation();
                
                AddDomainEvent(new AuctionEvent.ItemsReservationCancelled(
                    Id,
                    claim.ClaimedById,
                    currentTime));
            }
        }
        
        _availableQuantity = newQuantity;
    }
    
    public sealed record ClaimCommand(
        Guid ClaimedById,
        Quantity Quantity,
        string Comment = default);

    public sealed record ReserveCommand(
        Guid ClaimedById,
        bool TriggeredByUser = true);

    public sealed record HandOverCommand(
        Guid ClaimedById,
        Quantity? Quantity = null);
    
    private List<AuctionClaim> FindClaimsToBeCancelled(List<AuctionClaim> reservedClaims, Quantity newQuantity)
    {
        reservedClaims = reservedClaims
            .OrderBy(claim => claim.Timestamp)
            .ToList();
        
        int accumulatedQuantity = 0;
        List<AuctionClaim> claimsToCancel = new List<AuctionClaim>();
        
        foreach (AuctionClaim claim in reservedClaims)
        {
            if (accumulatedQuantity + claim.Quantity.Value <= newQuantity.Value)
            {
                accumulatedQuantity += claim.Quantity.Value;
            }
            else
            {
                claimsToCancel.Add(claim);
            }
        }

        return claimsToCancel;
    }
}