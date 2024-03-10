using Freethings.Offers.Domain.ValueObjects;

namespace Freethings.Offers.Domain.Entities;

public sealed class Offer
{
    public enum OfferState
    {
        Draft,
        Published
    }
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OfferState State { get; set; } = OfferState.Draft;
    public OfferTitle Title { get; set; }
    public OfferDescription Description { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
    
    private Offer()
    {
    }
    
    public Offer(Guid userId, OfferTitle title, OfferDescription description, int quantity)
    {
        UserId = userId;
        State = OfferState.Draft;
        Title = title;
        Description = description;
        Quantity = quantity;
    }
    
    public void Publish()
    {
        if (State == OfferState.Published)
        {
            return;
        }
        
        State = OfferState.Published;
        PublishedAt = DateTimeOffset.Now;
    }
    
}