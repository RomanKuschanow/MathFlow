#nullable disable

namespace MathFlow.TypeSystem.Types;
public class ValueType : InstantiateType
{
    private static ValueType _instance;

    public static ValueType Instance => _instance ??= new();

    private ValueType() : base("ValueType", TypeCategory.Class, Visibility.Public, null, Object.Instance, false, true)
    {
    }
}
