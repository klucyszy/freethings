namespace Freethings.Offers.Domain.Entities;

public sealed class OfferRequests
{
    public Guid Id { get; set; }
    public Guid OfferId { get; set; }
    public List<OfferRequest> Requests { get; set; } = new();
}

public sealed class OfferRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool Accepted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    private OfferRequest()
    {
    }
    
}