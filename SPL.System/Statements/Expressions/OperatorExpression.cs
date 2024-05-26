using SPL.System.Instances;
using SPL.System.Operators;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class OperatorExpression : IExpression
{
    private readonly List<IExpression> _operands;
    private readonly OperatorType _operatorType;
    private readonly Func<List<IType>, OperatorType, IOperator> _getOperator;

    public OperatorExpression(List<IExpression> operands, OperatorType operatorType, Func<List<IType>, OperatorType, IOperator> getOperator)
    {
        _operands = operands ?? throw new ArgumentNullException(nameof(operands));
        _operatorType = operatorType;
        _getOperator = getOperator ?? throw new ArgumentNullException(nameof(getOperator));
    }

    public async Task<IInstance<IType>> GetValue(CancellationToken ct)
    {
        var operands = _operands.Select(o => o.GetValue(ct).GetAwaiter().GetResult()).ToList();

        return await Task.FromResult(_getOperator(operands.Select(o => o.Type).ToList(), _operatorType).Calculate(operands));
    }
}
