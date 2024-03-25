using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure.Messaging;
using Freethings.Shared.Infrastructure.Time;

namespace Freethings.Shared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddModularSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventBus, EventBusPublisher>();
        services.AddCurrentTime();
        return services;
    }
}