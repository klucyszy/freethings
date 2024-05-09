using Microsoft.AspNetCore.Authentication;

namespace Freethings.Shared.Infrastructure.Authentication.ApiKey;

internal sealed class ApiKeyOptions : AuthenticationSchemeOptions
{
    public const string HeaderName = "ApiKey";
    public const string Scheme = "ApiKey";
    
    public string PrimaryValue { get; set; }
    public string SecondaryValue { get; set; }
}