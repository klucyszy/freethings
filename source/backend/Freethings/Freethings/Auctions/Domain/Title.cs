namespace Freethings.Auctions.Domain;

public record Title
{
    public string Value { get; }

    private Title(string value)
    {
        Value = value;
    }

    public static Title Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer title cannot be empty", nameof(value));
        }
        
        if (value.Length > 50)
        {
            throw new ArgumentException("Offer title cannot be longer than 100 characters", nameof(value));
        }

        return new Title(value);
    }
}