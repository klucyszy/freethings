namespace Freethings.Shared.Infrastructure.Authentication.ApiKey;

public static class Extensions
{
    public static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = ApiKeyDefaults.AuthenticationScheme;
            })
            .AddApiKey(options => { });

        services.AddAuthorization();

        return services;
    }
    
    public static IEndpointConventionBuilder RequireApiKeyAuthorization(this IEndpointConventionBuilder builder)
    {
        builder.RequireAuthorization(AuthSchemas.ApiKey);
        return builder;
    }
}