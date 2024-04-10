#nullable disable
using SPL.System.Types;

namespace SPL.System.Operators;
public class OperatorsManager
{
    private List<IOperator> Operators { get; init; } = new();

    public OperatorsManager(List<IOperator> operators)
    {
        if (operators is null)
        {
            throw new ArgumentNullException(nameof(operators));
        }

        if (operators.GroupBy(o => new { o.Operands, o.OperatorType }).Where(g => g.Count() > 1).SelectMany(g => g).Any())
            throw new InvalidDataException();

        Operators = operators;
    }

    public IOperator GetOperator(List<IType> args, OperatorType operatorType) => Operators.SingleOrDefault(o => o.Operands.SequenceEqual(args) && o.OperatorType == operatorType);
}
