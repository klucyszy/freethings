using System.Security.Claims;
using Freethings.Shared.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Freethings.Shared.Infrastructure.Authentication;

public static class Extensions
{
    public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        Auth0Options auth0Options = configuration.GetOptions<Auth0Options>("Shared:Authentication:Auth0");
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = auth0Options.Authority;
                options.Audience = auth0Options.Audience;   
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

        services.AddAuthorization();

        return services;
    }
    
    public static void UseAuth0(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}