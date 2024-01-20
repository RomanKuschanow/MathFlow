using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumNegation : Operator
{
    private static NumNegation _instance = new();

    public static NumNegation Instance => _instance;

    private NumNegation() : base(NumNegFunc.Instance.Arguments, NumNegFunc.Instance.Returns, OperatorType.Addition, NumNegFunc.Instance)
    {
    }

    private class NumNegFunc : IFunction
    {
        private static NumNegFunc _instance = new();

        public static NumNegFunc Instance => _instance;

        public List<Variable> Arguments => new()
        {
            new("a", Num.Instance),
        };

        public Type Returns => Num.Instance;

        public IInstance Execute(params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return -(NumInstance)instances[0];
        }
    }
}
