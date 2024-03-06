using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Infrastructure.Persistence;
using Freethings.Offers.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Freethings.Offers.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<OffersContext>(opts =>
        {
            opts.UseInMemoryDatabase("Offers");
        });

        services.AddScoped<IOfferRepository, OfferRepository>();
        
        return services;
    }
}