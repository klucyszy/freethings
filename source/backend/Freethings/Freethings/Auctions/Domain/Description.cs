namespace Freethings.Auctions.Domain;

public sealed record Description
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer description cannot be empty", nameof(value));
        }
        
        if (value.Length > 500)
        {
            throw new ArgumentException("Offer description cannot be longer than 500 characters", nameof(value));
        }

        return new Description(value);
    }
}