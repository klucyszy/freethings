using System.Security.Claims;
using Freethings.Shared.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Freethings.Shared.Infrastructure.Authentication.Auth0;

public static class Extensions
{
    public static AuthenticationBuilder AddAuth0(this AuthenticationBuilder authenticationBuilder,
        IConfiguration configuration)
    {
        Auth0Options auth0Options = configuration.GetOptions<Auth0Options>("Shared:Authentication:Auth0");
        
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.Authority = auth0Options.Authority;
            options.Audience = auth0Options.Audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        return authenticationBuilder;
    }
}