﻿#nullable disable
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

    public IOperator GetOperator(List<IType> args, OperatorType operatorType)
    {
        var op = Operators.SingleOrDefault(o => o.Operands.SingleOrDefault(_o => _o.SequenceEqual(args)) is not null && o.OperatorType == operatorType);

        return op is not null ? op : throw new InvalidDataException();
    }
}
