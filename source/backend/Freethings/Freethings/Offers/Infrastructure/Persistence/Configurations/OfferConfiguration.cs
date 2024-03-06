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
        
        builder.OwnsOne(e => e.Title, b =>
        {
            b.Property(e => e.Value)
                .HasConversion(
                    t => OfferTitle.Create(t),
                    t => t.Value)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        builder.OwnsOne(e => e.Description, b =>
        {
            b.Property(e => e.Value)
                .HasConversion(
                    t => OfferDescription.Create(t),
                    t => t.Value)
                .HasMaxLength(500)
                .IsRequired();
        });
    }
}