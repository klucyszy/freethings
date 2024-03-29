using Freethings.Auctions.Domain;
using Freethings.Auctions.Domain.Repositories;

namespace Freethings.Auctions.Infrastructure.Persistence.Repositories;

internal sealed class AuctionAdvertRepository : IAuctionAdvertRepository
{
    public Task<AuctionAdvert> AddAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuctionAdvert> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AuctionAdvert auctionAdvert, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}