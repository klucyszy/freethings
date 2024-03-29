using Freethings.Auctions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Auctions.Infrastructure.Persistence.Configurations;

internal sealed class AuctionAdvertConfiguration : IEntityTypeConfiguration<AuctionAdvert>
{
    public void Configure(EntityTypeBuilder<AuctionAdvert> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
        
        builder.Property(b => b.Quantity)
            .HasConversion(
                t => t.Value,
                t => Quantity.Create(t))
            .HasMaxLength(500)
            .HasColumnName(nameof(Quantity))
            .IsRequired();
        
        builder.Property(b => b.Title)
            .HasConversion(
                t => t.Value,
                t => Title.Create(t))
            .HasMaxLength(100)
            .HasColumnName(nameof(Title))
            .IsRequired();
        
        builder.Property(b => b.Description)
            .HasConversion(
                t => t.Value,
                t => Description.Create(t))
            .HasMaxLength(500)
            .HasColumnName(nameof(Description))
            .IsRequired();
        
        builder.HasMany(b => b.Claims)
            .WithOne(b => b.Auction)
            .HasForeignKey(b => b.AuctionId);
    }
}