namespace Freethings.Auctions.Domain.Strategies;

public interface IClaimSelectionPolicy
{
    Guid SelectClaimAsync(CancellationToken cancellationToken);
}

public sealed class RandomClaimSelectionPolicy : IClaimSelectionPolicy
{
    private static readonly Random _random = new();
    
    private readonly Auction _auction;

    public RandomClaimSelectionPolicy(Auction auction)
    {
        _auction = auction;
    }

    public Guid SelectClaimAsync(CancellationToken cancellationToken)
    {
        List<AuctionClaim> claims = _auction.Claims
            .Where(c => c.IsReserved == false)
            .ToList();

        if (claims.Count == 0)
        {
            return Guid.Empty;
        }

        int index = _random.Next(0, claims.Count);
        
        return claims[index].Id;
    }
}

public sealed class FirstComeFirstServedClaimSelectionPolicy : IClaimSelectionPolicy
{
    private readonly Auction _auction;

    public FirstComeFirstServedClaimSelectionPolicy(Auction auction)
    {
        _auction = auction;
    }

    public Guid SelectClaimAsync(CancellationToken cancellationToken)
    {
        AuctionClaim claim = _auction.Claims
            .OrderBy(c => c.Timestamp)
            .FirstOrDefault(c => c.IsReserved == false);

        return claim?.Id ?? Guid.Empty;
    }
}