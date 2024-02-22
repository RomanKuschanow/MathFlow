namespace MathFlow.TypeSystem.Instances;
public class StringInstance : IInstance
{
    public InstantiateType Type => Types.String.Instance;

    public string Value { get; set; }

    public StringInstance(string value)
    {
        Value = value;
    }

    public StringInstance() : this("") { }

    public override string ToString() => Value;

    public static StringInstance operator +(StringInstance a, StringInstance b) => new(a.Value + b.Value);
}
