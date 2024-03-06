using Freethings.Offers.Domain.Entities;

namespace Freethings.Offers.Domain.Repositories;

public interface IOfferRepository
{
    Task<Offer> GetAsync(Guid id, CancellationToken ct = default);
    
    Task<Offer> AddAsync(Offer offer, CancellationToken ct = default);
    Task<Offer> UpdateAsync(Offer offer, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}