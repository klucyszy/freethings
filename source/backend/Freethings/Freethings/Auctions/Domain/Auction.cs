using Freethings.Auctions.Domain.Exceptions;
using Freethings.Contracts.Events;

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
    private SelectionType _type;

    public Auction(int availableQuantity, SelectionType type)
    {
        _availableQuantity = availableQuantity;
        _type = type;
    }
    
    public AuctionEvent.ItemsClaimed Claim(ClaimItemsCommand command)
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

    public AuctionEvent.ItemsReserved ReserveItems(ReserveItemsCommand command)
    {
        AuctionClaim claim = _auctionClaims.FirstOrDefault(x => x.ClaimedById == command.ClaimedById);
        
        if (claim is null)
        {
            throw AuctionExceptions.CannotReserveItemsIfThereIsNoClaimReferenced.Exception;
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