using Freethings.Offers.Application.Entities.ValueObjects;

namespace Freethings.Offers.Application.Entities;

public sealed class Offer
{
    public enum OfferState
    {
        Draft,
        Published
    }

    public enum SelectionType
    {
        Manual,
        Random,
        FirstComeFirstServed
    }
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public SelectionType Type { get; set; }
    public OfferState State { get; set; } = OfferState.Draft;
    public OfferTitle Title { get; set; }
    public OfferDescription Description { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
    
    private Offer()
    {
    }
    
    public Offer(Guid userId, OfferTitle title, OfferDescription description,
        SelectionType selectionType = SelectionType.Manual, int quantity = 1)
    {
        UserId = userId;
        State = OfferState.Draft;
        Title = title;
        Description = description;
        Type = selectionType;
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