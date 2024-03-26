using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionMetadataEntityConfiguration : IEntityTypeConfiguration<AuctionMetadataEntity>
{
    public void Configure(EntityTypeBuilder<AuctionMetadataEntity> builder)
    {
        builder.ToTable("AuctionMetadata");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        
        builder.Property(b => b.Description)
            .HasConversion(
                t => t.Value,
                t => AuctionDescription.Create(t))
            .HasMaxLength(500)
            .HasColumnName(nameof(AuctionDescription))
            .IsRequired();
        
        builder.Property(b => b.Title)
            .HasConversion(
                t => t.Value,
                t => AuctionTitle.Create(t))
            .HasMaxLength(100)
            .HasColumnName(nameof(AuctionTitle))
            .IsRequired();
    }
}