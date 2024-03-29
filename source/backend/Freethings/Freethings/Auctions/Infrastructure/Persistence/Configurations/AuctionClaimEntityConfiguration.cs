using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionClaimEntityConfiguration : IEntityTypeConfiguration<AuctionClaim>
{
    public void Configure(EntityTypeBuilder<AuctionClaim> builder)
    {
        builder.ToTable("AuctionClaims");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().UseHiLo();
    }
}