namespace Freethings.Auctions.Domain;

public sealed record Quantity
{
    public int Value { get; }

    private Quantity(int value)
    {
        Value = value;
    }
    
    public static Quantity Create(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Quantity cannot be negative", nameof(value));
        }

        return new Quantity(value);
    }
    
    public static bool operator <(Quantity left, Quantity right)
    {
        return left.Value < right.Value;
    }
    
    public static bool operator >(Quantity left, Quantity right)
    {
        return left.Value > right.Value;
    }
    
    public static bool operator <=(Quantity left, Quantity right)
    {
        return left.Value <= right.Value;
    }
    
    public static bool operator >=(Quantity left, Quantity right)
    {
        return left.Value >= right.Value;
    }
    
    public static Quantity operator +(Quantity left, Quantity right)
    {
        return new Quantity(left.Value + right.Value);
    }
    
    public static Quantity operator -(Quantity left, Quantity right)
    {
        return new Quantity(left.Value - right.Value);
    }
}