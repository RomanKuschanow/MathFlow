using MathFlow.SemanticAnalyzer.Scope;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem;
public abstract class Type : IScope
{
    public string Name { get; init; }

    public TypeCategory Category { get; init; }

    public Visibility Visibility { get; init; }
    public ImmutableList<FieldDeclaration> Fields { get; }
    public IScope? ParentScope { get; init; }

    public Type(
        string name, 
        TypeCategory category,
        Visibility visibility,
        IEnumerable<FieldDeclaration> fields,
        IScope scope = null!)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        Category = category;
        Visibility = visibility;
        Fields = (fields ?? throw new ArgumentNullException(nameof(fields))).ToImmutableList();
        ParentScope = scope;
    }

    public IMember GetMember(Guid id) => throw new NotImplementedException();
}
