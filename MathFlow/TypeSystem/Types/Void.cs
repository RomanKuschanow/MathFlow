#nullable disable

namespace MathFlow.TypeSystem.Types;
public class Void : InstantiateType
{
    private static Void _instance;

    public static Void Instance => _instance ??= new();

    private Void() : base("Void", TypeCategory.Struct, Visibility.Public, null, ValueType.Instance, true, true)
    {
    }
}
