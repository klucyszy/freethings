using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionUserConfiguration : IEntityTypeConfiguration<AuctionUser>
{
    public void Configure(EntityTypeBuilder<AuctionUser> builder)
    {
        builder.ToTable("AuctionUsers");

        builder.HasKey(a => a.AppUserId);
        builder.Property(a => a.AppUserId).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
    }
}