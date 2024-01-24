using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem.Operators;
public class Operator : IFunction
{
    public Guid MemberId { get; init; } = Guid.NewGuid();

    public List<Variable> Arguments { get; init; }

    public Type Returns { get; init; }

    public OperatorType OperatorType { get; init; }

    public IFunction Function { get; init; }


    public Operator(IEnumerable<Variable> args, Type returns, OperatorType operatorType, IFunction function)
    {
        Arguments = (args ?? throw new ArgumentNullException(nameof(args))).ToList();
        Returns = returns ?? throw new ArgumentNullException(nameof(returns));
        OperatorType = operatorType;

        if (!function.Arguments.SequenceEqual(Arguments) || !function.Returns.Equals(Returns))
        {
            throw new ArgumentException($"Еhe function signature does not match the operator signature", nameof(function));
        }

        Function = function ?? throw new ArgumentNullException(nameof(function));
    }

    public IInstance Execute(IScope scope, params IInstance[] instances)
    {
        if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
        {
            throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
        }

        return Function.Execute(scope, instances);
    }
}
