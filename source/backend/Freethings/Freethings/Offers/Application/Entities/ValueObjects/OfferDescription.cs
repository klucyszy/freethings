namespace Freethings.Offers.Application.Entities.ValueObjects;

public sealed record OfferDescription
{
    public string Value { get; }

    private OfferDescription(string value)
    {
        Value = value;
    }

    public static OfferDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer description cannot be empty", nameof(value));
        }
        
        if (value.Length > 500)
        {
            throw new ArgumentException("Offer description cannot be longer than 500 characters", nameof(value));
        }

        return new OfferDescription(value);
    }
}