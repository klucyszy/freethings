using Freethings.Shared.Domain.Exceptions;

namespace Freethings.Auctions.Domain.Strategies;

public sealed class ManualClaimBehaviorStrategy : IClaimBehaviorStrategy
{
    public ClaimStrategyResult<AuctionClaim, DomainException> Claim(Auction.ClaimCommand command)
    {
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            command.Quantity,
            command.Comment,
            DateTimeOffset.Now,
            false
        );

        return ClaimStrategyResult<AuctionClaim, DomainException>.Success(claim);
    }
}