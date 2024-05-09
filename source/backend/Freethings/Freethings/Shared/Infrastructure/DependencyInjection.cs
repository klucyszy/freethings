using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure.Auth.Context;
using Freethings.Shared.Infrastructure.Authentication;
using Freethings.Shared.Infrastructure.Messaging;
using Freethings.Shared.Infrastructure.Middleware;

namespace Freethings.Shared.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddModularSharedInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEventBus, EventBusPublisher>();
        services.AddCurrentTime();
        
        services.AddExceptionHandler<BusinessExceptionHandler>();
        services.AddProblemDetails();

        services.AddAppAuthentication(configuration);
        services.AddCurrentUserContext();
        
        return services;
    }
}