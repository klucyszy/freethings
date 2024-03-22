namespace Freethings.Offers.Domain.ValueObjects;

public record OfferTitle
{
    public string Value { get; }

    private OfferTitle(string value)
    {
        Value = value;
    }

    public static OfferTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer title cannot be empty", nameof(value));
        }
        
        if (value.Length > 50)
        {
            throw new ArgumentException("Offer title cannot be longer than 100 characters", nameof(value));
        }

        return new OfferTitle(value);
    }
}