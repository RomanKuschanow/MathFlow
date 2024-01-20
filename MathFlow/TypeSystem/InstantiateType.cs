namespace MathFlow.TypeSystem;
public class InstantiateType : Type
{
    public bool IsSealed { get; init; }

    public bool IsSpecial { get; init; }

    public Type? Inherits { get; init; }

    public InstantiateType(
        string name,
        TypeCategory category,
        Visibility visibility, 
        IEnumerable<Field> fields,
        InstantiateType? inherits,
        bool isSealed,
        bool isSpecial)
        : base(name, category, visibility, fields)
    {
        if (inherits is not null)
        {
            if (inherits.IsSealed)
            {
                throw new ArgumentException($"'{name}': cannot derive from sealed type '{inherits.Name}'", nameof(inherits));
            }
        }

        Inherits = inherits;
        IsSealed = isSealed;
        IsSpecial = isSpecial;
    }
}
