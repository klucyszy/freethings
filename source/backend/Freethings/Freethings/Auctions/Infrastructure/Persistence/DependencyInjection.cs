using Freethings.Auctions.Domain;
using Freethings.Auctions.Infrastructure.Persistence.Repositories;
using Freethings.Shared.Abstractions.Domain;
using Freethings.Shared.Infrastructure.Persistence;

namespace Freethings.Auctions.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServerDbContext<AuctionsContext, AuctionContextOptions>(configuration, AuctionContextOptions.SectionName);
        
        services.AddScoped<IAggregateRootRepository<Auction>, AuctionsRepository>();
        
        return services;
    }
}