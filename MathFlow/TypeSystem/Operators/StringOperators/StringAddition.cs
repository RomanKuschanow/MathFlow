using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem.Operators.StringOperators;
public class StringAddition : Operator
{
    private static StringAddition _instance = new();

    public static StringAddition Instance => _instance;

    private StringAddition() : base(StringAddFunc.Instance.Arguments, StringAddFunc.Instance.Returns, OperatorType.Addition, StringAddFunc.Instance)
    {
    }

    private class StringAddFunc : IFunction
    {
        private static StringAddFunc _instance = new();

        public static StringAddFunc Instance => _instance;

        public List<Variable> Arguments => new()
        {
            new("a", Types.String.Instance),
            new("b", Types.String.Instance)
        };

        public Type Returns => Types.String.Instance;

        public IInstance Execute(params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return (StringInstance)instances[0] + (StringInstance)instances[1];
        }
    }
}
