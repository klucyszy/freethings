using Freethings.Contracts;
using Freethings.Contracts.Events;
using Freethings.Offers.Domain.Entities;
using Freethings.Shared.Exceptions;

namespace Freethings.Auctions.Domain;

public sealed class Auction
{
    public enum SelectionType
    {
        Manual,
        Random,
        FirstComeFirstServed
    }
    
    private readonly List<AuctionClaim> _auctionClaims = new();
    
    private int _availableQuantity;
    private Offer.SelectionType _type;

    public Auction(int availableQuantity, Offer.SelectionType type)
    {
        _availableQuantity = availableQuantity;
        _type = type;
    }
    
    public AuctionEvent.ItemsClaimed Claim(ClaimItemsCommand command)
    {
        if (_auctionClaims.Exists(x => x.ClaimedById == command.ClaimedById))
        {
            throw new DomainException("Same user cannot create two claims on one auction");
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

    public AuctionEvent.ItemsReserved ReserveItems(ReserveItemsCommand command)
    {
        AuctionClaim claim = _auctionClaims.FirstOrDefault(x => x.ClaimedById == command.ClaimedById);
        
        if (claim is null)
        {
            throw new DomainException("Cannot reserve items if there is no claim referenced");
        }
        
        claim.SelectAsReserved();
        
        return new AuctionEvent.ItemsReserved(
            claim.ClaimedById);
    }
    
    public sealed record ClaimItemsCommand(
        Guid ClaimedById,
        int Quantity,
        string Comment = default);

    public sealed record ReserveItemsCommand(
        Guid ClaimedById);
}