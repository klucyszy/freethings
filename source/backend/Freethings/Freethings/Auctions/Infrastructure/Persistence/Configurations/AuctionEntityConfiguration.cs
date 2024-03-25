using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionEntityConfiguration : IEntityTypeConfiguration<AuctionEntity>
{
    public void Configure(EntityTypeBuilder<AuctionEntity> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.HasOne(b => b.Metadata)
            .WithOne(b => b.Auction)
            .HasForeignKey<AuctionMetadataEntity>(b => b.AuctionId);
        
        builder.HasMany(b => b.Claims)
            .WithOne(b => b.Auction)
            .HasForeignKey(b => b.AuctionId);
    }
}