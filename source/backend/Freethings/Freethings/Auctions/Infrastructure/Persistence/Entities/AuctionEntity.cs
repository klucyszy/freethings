using Freethings.Auctions.Domain;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionEntity
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }
    public Auction.AuctionType Type { get; private set; }
    public AuctionMetadataEntity Metadata { get; private set; }
    public List<AuctionClaimEntity> Claims { get; private set; } = [];

    private AuctionEntity() {}
    
    public AuctionEntity(int quantity, Auction.AuctionType type, AuctionMetadataEntity metadata)
    {
        Quantity = quantity;
        Type = type;
        Metadata = metadata;
    }
    
    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }
}