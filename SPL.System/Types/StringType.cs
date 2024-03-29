using SPL.System.Instances;
using SPL.System.Operators;

namespace SPL.System.Types;
public class StringType : IType
{
    private static StringType _instance = new();

    public static StringType Instance => _instance;

    private StringType() { }

    public string Name => "String";

    public IEnumerable<IOperator> Operators => new List<IOperator>()
    {

    };

    public IInstance<IType> GetInstance(params object[] args)
    {
        throw new NotImplementedException();
    }

    public bool IsInstance(IInstance<IType> instance)
    {
        throw new NotImplementedException();
    }
}
