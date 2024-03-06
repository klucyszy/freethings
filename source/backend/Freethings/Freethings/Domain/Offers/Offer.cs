using Freethings.Domain.Offers.ValueObjects;

namespace Freethings.Domain.Offers;

public sealed class Offer
{
    public Guid Id { get; set; }
    public OfferTitle Title { get; set; }
    public OfferDescription Description { get; set; }
}