using SPL.System.Instances;
using SPL.System.Operators;

namespace SPL.System.Types;
public class BoolType : IType
{
    private static BoolType _instance = new();

    public static BoolType Instance => _instance;

    private BoolType() { }

    public string Name => "Bool";

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
