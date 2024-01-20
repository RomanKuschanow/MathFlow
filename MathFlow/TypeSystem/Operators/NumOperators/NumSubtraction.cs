using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumSubtraction : Operator
{
    private static NumSubtraction _instance = new();

    public static NumSubtraction Instance => _instance;

    private NumSubtraction() : base(NumSubFunc.Instance.Arguments, NumSubFunc.Instance.Returns, OperatorType.Addition, NumSubFunc.Instance)
    {
    }

    private class NumSubFunc : IFunction
    {
        private static NumSubFunc _instance = new();

        public static NumSubFunc Instance => _instance;

        public List<Variable> Arguments => new()
        {
            new("a", Num.Instance),
            new("b", Num.Instance)
        };

        public Type Returns => Num.Instance;

        public IInstance Execute(params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return (NumInstance)instances[0] - (NumInstance)instances[1];
        }
    }
}
