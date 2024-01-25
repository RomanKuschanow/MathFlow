using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumNegation : Operator
{
    private static NumNegation _instance = new();

    public static NumNegation Instance => _instance;

    private NumNegation() : base(NumNegFunc.Instance.Arguments, NumNegFunc.Instance.Returns, OperatorType.Negation, NumNegFunc.Instance)
    {
    }

    private class NumNegFunc : IFunction
    {
        public Guid MemberId { get; init; } = Guid.NewGuid();

        private static NumNegFunc _instance = new();

        public static NumNegFunc Instance => _instance;

        public ImmutableList<Variable> Arguments => new List<Variable>()
        {
            new("a", Num.Instance),
        }.ToImmutableList();

        public Type Returns => Num.Instance;

        public IInstance Execute(IScope scope = null!, params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return -(NumInstance)instances[0];
        }
    }
}
