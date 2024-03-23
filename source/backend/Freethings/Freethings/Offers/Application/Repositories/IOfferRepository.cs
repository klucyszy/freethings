using Freethings.Offers.Application.Entities;

namespace Freethings.Offers.Application.Repositories;

public interface IOfferRepository
{
    Task<Offer> GetAsync(Guid id, CancellationToken ct = default);
    
    Task<Offer> AddAsync(Offer offer, CancellationToken ct = default);
    Task<Offer> UpdateAsync(Offer offer, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}