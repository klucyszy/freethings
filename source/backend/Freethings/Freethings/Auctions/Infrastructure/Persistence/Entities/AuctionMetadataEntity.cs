using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionMetadataEntity
{
    public Guid Id { get; set; }
    public AuctionTitle Title { get; set; }
    public AuctionDescription Description { get; set; }
}