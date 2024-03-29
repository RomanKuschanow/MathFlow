using SPL.System.Instances;
using SPL.System.Operators;

namespace SPL.System.Types;
public interface IType
{
    string Name { get; }

    IEnumerable<IOperator> Operators { get; }

    IInstance<IType> GetInstance(params object[] args);

    bool IsInstance(IInstance<IType> instance);
}
