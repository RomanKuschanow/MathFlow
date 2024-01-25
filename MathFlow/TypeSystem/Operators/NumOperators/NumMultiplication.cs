using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumMultiplication : Operator
{
    private static NumMultiplication _instance = new();

    public static NumMultiplication Instance => _instance;

    private NumMultiplication() : base(NumMulFunc.Instance.Arguments, NumMulFunc.Instance.Returns, OperatorType.Multiplication, NumMulFunc.Instance)
    {
    }

    private class NumMulFunc : IFunction
    {
        public Guid MemberId { get; init; } = Guid.NewGuid();

        private static NumMulFunc _instance = new();

        public static NumMulFunc Instance => _instance;

        public ImmutableList<Variable> Arguments => new List<Variable>()
        {
            new("a", Num.Instance),
            new("b", Num.Instance)
        }.ToImmutableList();

        public Type Returns => Num.Instance;

        public IInstance Execute(IScope scope = null!, params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return (NumInstance)instances[0] * (NumInstance)instances[1];
        }
    }
}
