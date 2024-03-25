namespace Freethings.Auctions.Domain.Strategies;

public sealed record ClaimStrategyResult<TSuccess, TFailureException>
{
    public TSuccess Claim { get; init; }
    public TFailureException FailureReason { get; init; }
    public required bool CanBeClaimed { get; init; }
    
    public static ClaimStrategyResult<TSuccess, TFailureException> Success(TSuccess value) =>
        new()
        {
            Claim = value,
            CanBeClaimed = true
        };
    
    public static ClaimStrategyResult<TSuccess, TFailureException> Failure(TFailureException exception) =>
        new()
        {
            FailureReason = exception,
            CanBeClaimed = false
        };
}