#nullable disable

namespace MathFlow.TypeSystem.Types;
internal class String : InstantiateType
{
    private static String _instance;

    public static String Instance => _instance ??= new();

    private String() : base("String", TypeCategory.Class, Visibility.Public, null, ReferenceType.Instance, true, false)
    {
    }
}
