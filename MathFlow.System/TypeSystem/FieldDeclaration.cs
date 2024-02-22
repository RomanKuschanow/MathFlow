using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem;
public class FieldDeclaration
{
    public string Name { get; init; }
    public Type Type { get; init; }
    public Visibility Visibility { get; init; }
    public IInstance? DefaultValue { get; init; }

    public FieldDeclaration(string name, Type type, Visibility visibility = Visibility.Private, IInstance defaultValue = null!)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        Name = name;
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Visibility = visibility;
        DefaultValue = defaultValue;
    }

}
