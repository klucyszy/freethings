using Freethings.Shared.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;

namespace Freethings.Shared.Infrastructure.Authentication.ApiKey;

internal static class Extensions
{
    internal static AuthenticationBuilder AddApiKey(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
    {
        ApiKeyOptions apiKeyOptions = configuration.GetOptions<ApiKeyOptions>("Shared:Authentication:ApiKey");
        return authenticationBuilder.AddScheme<ApiKeyOptions, ApiKeyAuthenticationHandler>(ApiKeyOptions.Scheme,
            options =>
            {
                options.PrimaryValue = apiKeyOptions.PrimaryValue;
                options.SecondaryValue = apiKeyOptions.SecondaryValue;
            });
    }
    
    public static IEndpointConventionBuilder RequireApiKeyAuthorization(this IEndpointConventionBuilder builder)
    {
        builder.RequireAuthorization(AuthSchemas.ApiKey);
        return builder;
    }
}