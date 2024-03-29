namespace Freethings.Auctions.Domain.Repositories;

public interface IAuctionAdvertRepository
{
    Task<AuctionAdvert> AddAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default);
    Task<AuctionAdvert> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default);
}