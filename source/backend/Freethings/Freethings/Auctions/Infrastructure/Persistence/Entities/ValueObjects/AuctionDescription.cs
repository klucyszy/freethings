public sealed record AuctionDescription
{
    public string Value { get; }

    private AuctionDescription(string value)
    {
        Value = value;
    }

    public static AuctionDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Offer description cannot be empty", nameof(value));
        }
        
        if (value.Length > 500)
        {
            throw new ArgumentException("Offer description cannot be longer than 500 characters", nameof(value));
        }

        return new AuctionDescription(value);
    }
}