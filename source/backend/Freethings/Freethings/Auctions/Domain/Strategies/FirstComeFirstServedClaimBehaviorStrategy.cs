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
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            command.Quantity,
            command.Comment,
            DateTimeOffset.Now,
            _availableQuantity >= command.Quantity
        );

        return ClaimStrategyResult<AuctionClaim, DomainException>.Success(claim);
    }
}