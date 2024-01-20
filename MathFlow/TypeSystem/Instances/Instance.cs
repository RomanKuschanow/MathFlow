using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Instances;
public class Instance : IInstance
{
    public InstantiateType Type { get; init; }

    private Dictionary<Field, Instance> Fields { get; init; }

    public Instance(InstantiateType type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Fields = type.Fields.Select(f => new KeyValuePair<Field, Instance>(f, f.DefaultValue)).ToDictionary(f => f.Key, f => f.Value);
    }

    public Instance GetValue(Field field) => Fields[field];
    public void SetValue(Field field, Instance instance)
    {
        if (!field.Type.Equals(instance.Type))
        {
            throw new ArgumentException($"Cannot convert '{instance.Type}' to '{instance.Type}'", nameof(instance));
        }

        Fields[field] = instance;
    }
}
