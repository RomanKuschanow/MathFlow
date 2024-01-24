using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumAddition : Operator
{
    private static NumAddition _instance = new();

    public static NumAddition Instance => _instance;

    private NumAddition() : base(NumAddFunc.Instance.Arguments, NumAddFunc.Instance.Returns, OperatorType.Addition, NumAddFunc.Instance)
    {
    }

    private class NumAddFunc : IFunction
    {
        public Guid MemberId { get; init; } = Guid.NewGuid();

        private static NumAddFunc _instance = new();

        public static NumAddFunc Instance => _instance;

        public List<Variable> Arguments => new()
        {
            new("a", Num.Instance),
            new("b", Num.Instance)
        };

        public Type Returns => Num.Instance;

        public IInstance Execute(IScope scope = null!, params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return (NumInstance)instances[0] + (NumInstance)instances[1];
        }
    }
}
