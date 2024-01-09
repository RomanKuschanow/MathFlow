namespace MathFlow.SemanticAnalyzer.Datatypes;
public struct Num
{
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

    public Num(dynamic value)
    {
        if (!(value is double || value is decimal)) throw new ArgumentException("", nameof(value));

        _value = value;
    }

    public Num() : this(0d) { }

    public override string ToString() => $"{Value}";

    public static Num operator +(Num a, Num b)
    {
        if (a.GetType() == b.GetType())
            return new(a.Value + b.Value);
        else
            return new((decimal)a.Value + (decimal)b.Value);
    }

    public static Num operator -(Num a, Num b)
    {
        if (a.GetType() == b.GetType())
            return new(a.Value - b.Value);
        else
            return new((decimal)a.Value - (decimal)b.Value);
    }

    public static Num operator *(Num a, Num b)
    {
        if (a.GetType() == b.GetType())
            return new(a.Value * b.Value);
        else
            return new((decimal)a.Value * (decimal)b.Value);
    }

    public static Num operator /(Num a, Num b)
    {
        if (a.GetType() == b.GetType())
            return new(a.Value / b.Value);
        else
            return new((decimal)a.Value / (decimal)b.Value);
    }

    public static Num operator -(Num a) => new(-a.Value);
}
