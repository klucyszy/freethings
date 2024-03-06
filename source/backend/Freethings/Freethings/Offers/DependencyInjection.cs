using Freethings.Offers.Application;
using Freethings.Offers.Domain;
using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.Repositories;
using Freethings.Offers.Infrastructure;
using Freethings.Offers.Presentation;

namespace Freethings.Offers;

public static class DependencyInjection
{
    public static IServiceCollection AddOffers(this IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation();
        
        return services;
    }
    
    public static WebApplication MapOffersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/offers");
        
        group.MapGet("{id:guid}", (Guid id, IOfferRepository repository) => repository.GetAsync(id));
        group.MapPost("", (Offer offer, IOfferRepository repository) => repository.AddAsync(offer));
        group.MapPut("", (Offer offer, IOfferRepository repository) => repository.UpdateAsync(offer));
        group.MapDelete("{id:guid}", (Guid id, IOfferRepository repository) => repository.DeleteAsync(id));
        
        return app;
    }
}