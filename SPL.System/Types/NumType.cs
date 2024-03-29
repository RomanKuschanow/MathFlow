using SPL.System.Instances;
using SPL.System.Operators;

namespace SPL.System.Types;
public class NumType : IType
{
    private static NumType _instance = new();

    public static NumType Instance => _instance;

    private NumType() { }

    public string Name => "Num";

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
