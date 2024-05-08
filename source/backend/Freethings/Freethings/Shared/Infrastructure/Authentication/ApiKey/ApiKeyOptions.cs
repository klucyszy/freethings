using Microsoft.AspNetCore.Authentication;

namespace Freethings.Shared.Infrastructure.Authentication.ApiKey;

internal sealed class ApiKeyOptions : AuthenticationSchemeOptions
{
    public const string HeaderName = "ApiKey";
    public const string Scheme = "ApiKey";
    
    public string PrimaryValue { get; init; }
    public string SecondaryValue { get; init; }
}