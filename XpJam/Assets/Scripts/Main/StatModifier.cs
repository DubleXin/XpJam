
public enum StatModType
{
    Flat = 10,
    PercentPlus = 20,
    PercentMult= 30
}

public class StatModifier
{
    public readonly float ModValue;
    public readonly StatModType Type;
    public readonly int Order;

    public StatModifier(float value, StatModType type, int order)
    {
        ModValue = value;
        Type = type;
        Order = order;
    }
    public StatModifier(float value, StatModType type) : this(value, type, (int)type) { }  
}
