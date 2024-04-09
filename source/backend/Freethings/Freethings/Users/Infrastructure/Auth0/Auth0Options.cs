namespace Freethings.Users.Infrastructure.Auth0;

public sealed record Auth0Options
{
    public const string SectionName = "Auth0";
    
    public string BaseUrl { get; init; }
    
    // TODO: Remove and use proper authentication
    public string ManagementApiToken { get; init; }
}