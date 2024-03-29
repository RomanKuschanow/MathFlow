using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Operators;
public interface IOperator
{
    List<IType> Operands { get; }

    IType ResultType { get; }

    IInstance Calculate(List<IInstance> args);
}
