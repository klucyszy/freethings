using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Infrastructure.Persistence;
using Freethings.Offers.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure;

public static class DependencyInjection
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
        
        services.AddScoped<IOfferRepository, OfferRepository>();
        
        return services;
    }
}