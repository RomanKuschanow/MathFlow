#nullable disable


namespace MathFlow.TypeSystem.Types;
public class Object : InstantiateType
{
    private static Object _instance;

    public static Object Instance => _instance ??= new(); 

    public Object() : base("Object", TypeCategory.Class, Visibility.Public, null, null, false, false)
    {
    }
}
