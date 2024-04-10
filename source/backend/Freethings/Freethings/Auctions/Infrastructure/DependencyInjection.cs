using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;
using Freethings.Auctions.Infrastructure.Persistence;
using Freethings.Auctions.Infrastructure.Persistence.Repositories;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Abstractions.Persistence;
using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSqlServerDbContext<AuctionsContext, AuctionContextOptions>(configuration,
            AuctionContextOptions.ModuleName);

        services.AddScoped<IDataSeeder, AuctionsSeeder>();
        services.AddScoped<IAggregateRootRepository<AuctionAggregate>, AuctionAggregateRepository>();
        services.AddScoped<IAuctionAdvertRepository, AuctionAdvertRepository>();
        
        return services;
    }
}