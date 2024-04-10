using Freethings.Users.Presentation.Endpoints;

namespace Freethings.Users.Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication MapUsersEndpoints(this WebApplication app)
    {
        app
            .MapGroup("api/users")
            .MapCreateApplicationUserEndpoint()
            .WithTags("Users");
        
        return app;
    }
}