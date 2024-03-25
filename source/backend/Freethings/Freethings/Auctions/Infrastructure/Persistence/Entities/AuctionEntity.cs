using Freethings.Auctions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Auction.AuctionType Type { get; set; }
    public AuctionMetadataEntity MetadataEntity { get; set; }
    public List<AuctionClaimEntity> Claims { get; set; } = [];
}