using Freethings.Shared.Infrastructure.Authentication.ApiKey;
using Freethings.Shared.Infrastructure.Authentication.Auth0;

namespace Freethings.Shared.Infrastructure.Authentication;

internal static class Extensions
{
    internal static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = AuthSchemas.Bearer;
                options.DefaultAuthenticateScheme = AuthSchemas.Bearer;
            })
            .AddApiKey(configuration)
            .AddAuth0(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthSchemas.ApiKey, policy =>
            {
                policy.AddAuthenticationSchemes(AuthSchemas.ApiKey);
                policy.RequireAuthenticatedUser();
            });
            options.AddPolicy(AuthSchemas.Bearer, policy =>
            {
                policy.AddAuthenticationSchemes(AuthSchemas.Bearer);
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
    internal static IApplicationBuilder UseAppAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}