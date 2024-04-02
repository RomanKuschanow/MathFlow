using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Operators;
public class Operator : IOperator
{
    public List<IType> Operands { get; init; }

    public IType ResultType { get; init; }

    public OperatorType OperatorType { get; init; }

    private Func<List<IInstance<IType>>, IInstance<IType>> _calculate { get; init; }

    public Operator(List<IType> operandTypes, IType resultType, OperatorType operatorType, Func<List<IInstance<IType>>, IInstance<IType>> func)
    {
        Operands = operandTypes;
        ResultType = resultType;
        OperatorType = operatorType;
        _calculate = func;
    }

    public IInstance<IType> Calculate(List<IInstance<IType>> args) => _calculate(args);
}
