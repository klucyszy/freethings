using Freethings.Auctions.Domain.Strategies.Abstractions;
using Freethings.Shared.Abstractions.Domain.Exceptions;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Domain.Strategies;

public sealed class ManualClaimBehaviorStrategy : IClaimBehaviorStrategy
{
    private readonly ICurrentTime _currentTime;

    public ManualClaimBehaviorStrategy(ICurrentTime currentTime)
    {
        _currentTime = currentTime;
    }

    public ClaimStrategyResult<AuctionClaim, DomainException> Claim(Auction.ClaimCommand command)
    {
        AuctionClaim claim = new AuctionClaim(
            command.ClaimedById,
            command.Quantity,
            command.Comment,
            _currentTime.UtcNow(),
            false
        );

        return ClaimStrategyResult<AuctionClaim, DomainException>.Success(claim);
    }
}