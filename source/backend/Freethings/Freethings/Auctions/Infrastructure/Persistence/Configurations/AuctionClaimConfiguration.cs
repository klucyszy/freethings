using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionClaimConfiguration : IEntityTypeConfiguration<AuctionClaim>
{
    public void Configure(EntityTypeBuilder<AuctionClaim> builder)
    {
        builder.ToTable("AuctionClaims");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
        
        builder.Property(b => b.Quantity)
            .HasConversion(
                t => t.Value,
                t => Quantity.Create(t))
            .HasMaxLength(500)
            .HasColumnName(nameof(Quantity))
            .IsRequired();
    }
}