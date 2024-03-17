using Freethings.Shared.Domain.Exceptions;

namespace Freethings.Auctions.Domain.Strategies;

public interface IClaimBehaviorStrategy
{
    public ClaimStrategyResult<AuctionClaim, DomainException> Claim(Auction.ClaimCommand command);
}