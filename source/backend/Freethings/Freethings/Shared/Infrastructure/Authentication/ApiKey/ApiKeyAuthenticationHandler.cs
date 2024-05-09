using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Freethings.Shared.Infrastructure.Authentication.ApiKey;

internal sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyOptions>
{
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues apiKeyHeaderValues))
        {
            return Task.FromResult(AuthenticateResult.Fail($"{HeaderNames.Authorization} was not provided."));
        }

        string providedApiKey = apiKeyHeaderValues
            .FirstOrDefault()
            ?.Replace(AuthSchemas.ApiKey, string.Empty)
            ?.Trim();

        if (string.IsNullOrWhiteSpace(providedApiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail($"Invalid {ApiKeyOptions.HeaderName}."));
        }
        
        if (!providedApiKey.Equals(Options.PrimaryValue, StringComparison.Ordinal)
            && !providedApiKey.Equals(Options.SecondaryValue, StringComparison.Ordinal))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }
        
        Claim[] claims = [new Claim(ClaimTypes.Name, providedApiKey)];
        ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}