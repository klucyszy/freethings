using Freethings.Auctions.Domain.Strategies.Abstractions;
using Freethings.Shared.Abstractions.Domain.Exceptions;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Auctions.Domain.Strategies;

public sealed class FirstComeFirstServedClaimBehaviorStrategy : IClaimBehaviorStrategy
{
    private readonly int _availableQuantity;
    private readonly ICurrentTime _currentTime;
    
    public FirstComeFirstServedClaimBehaviorStrategy(int availableQuantity, ICurrentTime currentTime)
    {
        _availableQuantity = availableQuantity;
        _currentTime = currentTime;
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
            _currentTime.Now(),
            reserved
        );

        return ClaimStrategyResult<AuctionClaim, DomainException>.Success(claim);
    }
}