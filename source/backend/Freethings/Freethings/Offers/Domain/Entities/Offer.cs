using Freethings.Offers.Domain.ValueObjects;

namespace Freethings.Offers.Domain.Entities;

public sealed class Offer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OfferTitle Title { get; set; }
    public OfferDescription Description { get; set; }
    
    private Offer()
    {
    }
    
    public Offer(Guid userId, OfferTitle title, OfferDescription description)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}