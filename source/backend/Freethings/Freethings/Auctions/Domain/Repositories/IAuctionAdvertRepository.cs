using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Auctions.Domain.Repositories;

public interface IAuctionAdvertRepository
{
    Task<AuctionAdvert> AddAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default);
    Task<AuctionAdvert> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<IDomainEvent>> UpdateAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid auctionAdvertId, CancellationToken cancellationToken);
}