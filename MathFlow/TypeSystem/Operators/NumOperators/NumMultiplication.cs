using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumMultiplication : Operator
{
    private static NumMultiplication _instance = new();

    public static NumMultiplication Instance => _instance;

    private NumMultiplication() : base(NumMulFunc.Instance.Arguments, NumMulFunc.Instance.Returns, OperatorType.Addition, NumMulFunc.Instance)
    {
    }

    private class NumMulFunc : IFunction
    {
        private static NumMulFunc _instance = new();

        public static NumMulFunc Instance => _instance;

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

            return (NumInstance)instances[0] * (NumInstance)instances[1];
        }
    }
}
