using Freethings.Auctions.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionClaimEntityConfiguration : IEntityTypeConfiguration<AuctionClaimEntity>
{
    public void Configure(EntityTypeBuilder<AuctionClaimEntity> builder)
    {
        builder.ToTable("AuctionClaims");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
    }
}