using Freethings.Auctions.Domain.Exceptions;
using Freethings.Auctions.Domain.Strategies;
using Freethings.Auctions.Domain.Strategies.ClaimedItemsReservationStrategy;
using Freethings.Contracts.Events;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Domain;

public sealed class Auction : AggregateRoot
{
    public IReadOnlyCollection<AuctionClaim> Claims => _auctionClaims.AsReadOnly();
    public int AvailableQuantity => _availableQuantity;
    
    private readonly ICurrentTime _currentTime;

    private readonly List<AuctionClaim> _auctionClaims;
    private readonly AuctionType _auctionType;
    private int _availableQuantity;

    public enum AuctionType
    {
        Manual,
        FirstComeFirstServed
    }

    public Auction(Guid id, List<AuctionClaim> auctionClaims, int availableQuantity, AuctionType auctionType,
        ICurrentTime currentTime)
    {
        Id = id;
        _auctionClaims = auctionClaims;
        _availableQuantity = availableQuantity;
        _auctionType = auctionType;
        _currentTime = currentTime;
    }

    public void Claim(ClaimCommand command)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            throw AuctionExceptions.SameUserCannotCreateTwoClaimsOnOneAuction.Exception;
        }
        
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            command.Quantity,
            command.Comment,
            _currentTime.UtcNow(),
            false
        );

        _auctionClaims.Add(claim);

        AddDomainEvent(new AuctionEvent.ItemsClaimed(
            Id,
            claim.ClaimedById,
            claim.Quantity,
            claim.Timestamp.Value));
    }

    public void Reserve(ReserveCommand command)
    {
        AuctionClaim claim = _auctionClaims
            .FirstOrDefault(x => x.ClaimedById == command.ClaimedById);

        if (claim is null)
        {
            throw AuctionExceptions.CannotReserveIfThereIsNoClaimReferenced.Exception;
        }
        
        if (claim.IsReserved)
        {
            return;
        }
        
        IClaimedItemsReservationStrategy reservationStrategy = command.TriggeredByUser
            ? new AlwaysAllowReservationStrategy()
            : ClaimedItemsReservationStrategyFactory.Create(_auctionType);
        
        if (!reservationStrategy.CanReserve())
        {
            return;
        }
        
        if (_availableQuantity < claim.Quantity)
        {
            throw AuctionExceptions.AvailableQuantitySmallerThanAvailable.Exception;
        }

        claim.MarkAsReserved();
        
        AddDomainEvent(new AuctionEvent.ItemsReserved(
            Id,
            claim.ClaimedById,
            claim.Quantity,
            claim.Timestamp.Value));
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
            Id,
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
        Guid ClaimedById,
        bool TriggeredByUser = true);

    public sealed record HandOverCommand(
        Guid ClaimedById,
        int? Quantity = null);
}