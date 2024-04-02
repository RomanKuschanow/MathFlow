using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Operators;
public interface IOperator
{
    List<IType> Operands { get; }

    IType ResultType { get; }

    OperatorType OperatorType { get; }

    IInstance<IType> Calculate(List<IInstance<IType>> args);
}
