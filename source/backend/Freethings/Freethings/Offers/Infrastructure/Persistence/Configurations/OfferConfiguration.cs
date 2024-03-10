using Freethings.Offers.Domain.Entities;
using Freethings.Offers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freethings.Offers.Infrastructure.Persistence.Configurations;

public sealed class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        
        builder.Property(b => b.Description)
            .HasConversion(
                t => t.Value,
                t => OfferDescription.Create(t))
            .HasMaxLength(500)
            .HasColumnName(nameof(OfferDescription))
            .IsRequired();
        
        builder.Property(b => b.Title)
            .HasConversion(
                t => t.Value,
                t => OfferTitle.Create(t))
            .HasMaxLength(100)
            .HasColumnName(nameof(OfferTitle))
            .IsRequired();
    }
}