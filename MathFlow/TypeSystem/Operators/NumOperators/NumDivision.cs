using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumDivision : Operator
{
    private static NumDivision _instance = new();

    public static NumDivision Instance => _instance;

    private NumDivision() : base(NumDivFunc.Instance.Arguments, NumDivFunc.Instance.Returns, OperatorType.Addition, NumDivFunc.Instance)
    {
    }

    private class NumDivFunc : IFunction
    {
        private static NumDivFunc _instance = new();

        public static NumDivFunc Instance => _instance;

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

            return (NumInstance)instances[0] / (NumInstance)instances[1];
        }
    }
}
