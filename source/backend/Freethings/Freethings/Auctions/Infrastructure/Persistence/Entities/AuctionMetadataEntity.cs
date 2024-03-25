using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;

namespace Freethings.Auctions.Infrastructure.Persistence.Entities;

internal sealed class AuctionMetadataEntity
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public AuctionTitle Title { get; private set; }
    public AuctionDescription Description { get; private set; }
    
    public AuctionEntity Auction { get; private set; }
    
    private AuctionMetadataEntity() {}
    
    public AuctionMetadataEntity(AuctionTitle title, AuctionDescription description)
    {
        Title = title;
        Description = description;
    }
}