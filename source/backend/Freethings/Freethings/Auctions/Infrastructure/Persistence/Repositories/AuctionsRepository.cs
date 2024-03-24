using Freethings.Auctions.Domain;
using Freethings.Shared.Abstractions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Repositories;

internal sealed class AuctionsRepository : IAggregateRootRepository<Auction>
{
    public Task<Auction> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}