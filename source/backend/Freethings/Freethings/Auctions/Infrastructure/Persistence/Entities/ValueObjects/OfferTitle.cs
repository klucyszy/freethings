namespace Freethings.Auctions.Infrastructure.Persistence.Entities.ValueObjects;

public record AuctionTitle
{
    public string Value { get; }

    private AuctionTitle(string value)
    {
        Value = value;
    }

    public static AuctionTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer title cannot be empty", nameof(value));
        }
        
        if (value.Length > 50)
        {
            throw new ArgumentException("Offer title cannot be longer than 100 characters", nameof(value));
        }

        return new AuctionTitle(value);
    }
}