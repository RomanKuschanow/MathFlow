using MathFlow.SemanticAnalyzer.Scope;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Instances;
public class Instance : IInstance, IScope
{
    public InstantiateType Type { get; init; }

    private ImmutableList<Field> Fields { get; init; }

    public IScope ParentScope => throw new NotImplementedException();

    public Instance(InstantiateType type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Fields = type.Fields.Select(f => new Field(f.Name, f.Type, f.Visibility, f.DefaultValue)).ToImmutableList();
    }

    public IInstance? GetValue(Guid id) => ((Field)GetMember(id)).Value;
    public void SetValue(Guid id, IInstance instance)
    {
        var field = (Field)GetMember(id);

        if (!field.Type.Equals(instance.Type))
        {
            throw new ArgumentException($"Cannot convert '{instance.Type}' to '{instance.Type}'", nameof(instance));
        }

        field.Value = instance;
    }

    public IMember GetMember(Guid id) => Fields.Where(f => f.Visibility == Visibility.Public).Cast<IMember>().Single(m => m.MemberId == id);
}
