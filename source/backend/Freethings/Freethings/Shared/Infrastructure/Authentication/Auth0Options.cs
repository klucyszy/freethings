namespace Freethings.Shared.Infrastructure.Authentication;

public sealed record Auth0Options
{
    public string Authority { get; init; }
    public string Audience { get; init; }
}