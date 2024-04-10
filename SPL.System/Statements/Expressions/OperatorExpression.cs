using SPL.System.Instances;
using SPL.System.Operators;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class OperatorExpression : IExpression
{
    private readonly List<IExpression> _operands;
    private readonly IOperator _operator;

    public OperatorExpression(List<IExpression> operands, IOperator @operator)
    {
        _operands = operands ?? throw new ArgumentNullException(nameof(operands));
        _operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
    }

    public IInstance<IType> GetValue() => _operator.Calculate(_operands.Select(o => o.GetValue()).ToList());
}
