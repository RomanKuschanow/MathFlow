using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem;
public class Field : Variable
{
    public Visibility Visibility { get; init; }
    public IInstance? DefaultValue {  get; init; }

    public Field(string name, Type type, Visibility visibility, IInstance? defaultValue = null) : base(name, type, defaultValue)
    {
        Visibility = visibility;
        DefaultValue = defaultValue;
    }
}
