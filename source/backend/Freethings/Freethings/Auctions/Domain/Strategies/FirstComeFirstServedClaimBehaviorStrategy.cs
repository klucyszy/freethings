using Freethings.Shared.Domain.Exceptions;

namespace Freethings.Auctions.Domain.Strategies;

public sealed class FirstComeFirstServedClaimBehaviorStrategy : IClaimBehaviorStrategy
{
    private readonly int _availableQuantity;

    public FirstComeFirstServedClaimBehaviorStrategy(int availableQuantity)
    {
        _availableQuantity = availableQuantity;
    }

    public ClaimStrategyResult<AuctionClaim, DomainException> Claim(Auction.ClaimCommand command)
    {
        int claimQuantity = command.Quantity;
        if (_availableQuantity > 0 && _availableQuantity < command.Quantity)
        {
            claimQuantity = _availableQuantity;
        }

        bool reserved = _availableQuantity > 0;
        
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            claimQuantity,
            command.Comment,
            DateTimeOffset.Now,
            reserved
        );

        return ClaimStrategyResult<AuctionClaim, DomainException>.Success(claim);
    }
}