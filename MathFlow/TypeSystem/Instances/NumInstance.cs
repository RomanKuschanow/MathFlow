using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Instances;
public class NumInstance : IInstance
{
    public InstantiateType Type => Num.Instance;

    private dynamic _value;

    public dynamic Value
    {
        get => _value;
        set
        {
            if (!(value is double || value is decimal)) throw new ArgumentException("", nameof(value));

            _value = value;
        }
    }

    public NumInstance(dynamic value)
    {
        if (!(value is double || value is decimal)) throw new ArgumentException("", nameof(value));

        _value = value;
    }

    public NumInstance() : this(0d) { }

    public override string ToString() => $"{Value}";

    public static NumInstance operator +(NumInstance a, NumInstance b)
    {
        if (a.Value.GetType() == b.Value.GetType())
            return new(a.Value + b.Value);
        else
            return new((decimal)a.Value + (decimal)b.Value);
    }

    public static NumInstance operator -(NumInstance a, NumInstance b)
    {
        if (a.Value.GetType() == b.Value.GetType())
            return new(a.Value - b.Value);
        else
            return new((decimal)a.Value - (decimal)b.Value);
    }

    public static NumInstance operator *(NumInstance a, NumInstance b)
    {
        if (a.Value.GetType() == b.Value.GetType())
            return new(a.Value * b.Value);
        else
            return new((decimal)a.Value * (decimal)b.Value);
    }

    public static NumInstance operator /(NumInstance a, NumInstance b)
    {
        if (a.Value.GetType() == b.Value.GetType())
            return new(a.Value / b.Value);
        else
            return new((decimal)a.Value / (decimal)b.Value);
    }

    public static NumInstance operator -(NumInstance a) => new(-a.Value);
}
