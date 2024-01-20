#nullable disable

namespace MathFlow.TypeSystem.Types;
public class Num : InstantiateType
{
    private static Num _instance;

    public static Num Instance => _instance ??= new();

    private Num() : base("Num", TypeCategory.Struct, Visibility.Public, null, ValueType.Instance, true, false)
    {
    }
}
