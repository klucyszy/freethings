using Freethings.Offers.Application.Repositories;
using Freethings.Offers.Infrastructure.Persistence;
using Freethings.Offers.Infrastructure.Persistence.Repositories;
using Freethings.Shared.Abstractions.Messaging;
using Freethings.Shared.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OffersContext>(opts =>
        {
            opts.UseInMemoryDatabase("Offers");
        });
        // services.AddDbContext<OffersContext>(opts =>
        // {
        //     opts.UseSqlServer(
        //         configuration["ConnectionStrings:Offers"],
        //         sqlOptions =>
        //         {
        //             sqlOptions.MigrationsHistoryTable(
        //                 "__EFMigrationHistory_Offers",
        //                 "offers");
        //         });
        // });

        services.AddScoped<IEventBus, EventBusPublisher>();
        
        services.AddScoped<IOfferRepository, OfferRepository>();
        
        return services;
    }
}