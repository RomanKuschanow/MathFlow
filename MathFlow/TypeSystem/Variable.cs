using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem;
public class Variable : IMember
{
    public Guid MemberId { get; init; } = Guid.NewGuid();

    public string Name { get; init; }
    public Type Type { get; init; }
    public IInstance? Value { get; set; }

    public Variable(string name, Type type, IInstance? value = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        Type = type ?? throw new ArgumentNullException(nameof(type));

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Variable) return false;
        if (ReferenceEquals(this, obj)) return true;
        if ((obj as Variable)!.Name != Name) return false;
        if ((obj as Variable)!.Type != Type) return false;

        return true;
    }

    public override int GetHashCode() => base.GetHashCode();
}
