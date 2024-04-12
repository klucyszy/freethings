namespace Freethings.Users.Infrastructure.Auth0;

public sealed record Auth0Options
{
    public const string SectionName = "Users:Auth0";
    
    public string BaseUrl { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string Audience { get; init; }
}