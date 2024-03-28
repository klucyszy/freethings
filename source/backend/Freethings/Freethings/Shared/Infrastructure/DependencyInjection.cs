using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure.Messaging;

namespace Freethings.Shared.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddModularSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventBus, EventBusPublisher>();
        services.AddCurrentTime();
        
        return services;
    }
}