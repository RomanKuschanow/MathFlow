using System.Collections.Immutable;

namespace MathFlow.TypeSystem;
public abstract class Type
{
    public string Name { get; init; }

    public TypeCategory Category { get; init; }

    public Visibility Visibility { get; init; }

    public ImmutableList<Field> Fields { get; init; }

    public Type(
        string name, 
        TypeCategory category,
        Visibility visibility,
        IEnumerable<Field> fields)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        Category = category;
        Visibility = visibility;
        Fields = (fields ?? new List<Field>()).ToImmutableList();
    }

    public ImmutableList<Field> GetPublicFields() => Fields.Where(f => f.Visibility == Visibility.Public).ToImmutableList();
}
