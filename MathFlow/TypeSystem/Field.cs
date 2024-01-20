using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem;
public class Field : Variable
{
    public Visibility Visibility { get; init; }
    public Instance DefaultValue {  get; init; }

    public Field(string name, Type type, Visibility visibility, Instance defaultValue) : base(name, type)
    {
        Visibility = visibility;
        DefaultValue = defaultValue;
    }
}
