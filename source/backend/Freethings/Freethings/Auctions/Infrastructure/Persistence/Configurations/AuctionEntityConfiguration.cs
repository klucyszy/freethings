using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionEntityConfiguration : IEntityTypeConfiguration<AuctionAdvert>
{
    public void Configure(EntityTypeBuilder<AuctionAdvert> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().UseHiLo();
        
        builder.HasMany(b => b.Claims)
            .WithOne(b => b.Auction)
            .HasForeignKey(b => b.AuctionId);
    }
}