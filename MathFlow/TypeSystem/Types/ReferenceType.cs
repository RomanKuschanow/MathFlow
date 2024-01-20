#nullable disable

namespace MathFlow.TypeSystem.Types;
public class ReferenceType : InstantiateType
{
    private static ReferenceType _instance;

    public static ReferenceType Instance => _instance ??= new();

    private ReferenceType() : base("ReferenceType", TypeCategory.Class, Visibility.Public, null, Object.Instance, false, true)
    {
    }
}
