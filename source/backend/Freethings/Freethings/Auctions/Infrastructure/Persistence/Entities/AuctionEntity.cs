using Freethings.Auctions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Auction.AuctionType Type { get; set; }
    public AuctionMetadataEntity Metadata { get; set; }
    public List<AuctionClaimEntity> Claims { get; set; } = [];

    private AuctionEntity() {}
    
    public AuctionEntity(int quantity, Auction.AuctionType type, AuctionMetadataEntity metadata)
    {
        Quantity = quantity;
        Type = type;
        Metadata = metadata;
    }
}