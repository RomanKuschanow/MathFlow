using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;

namespace MathFlow.TypeSystem.Operators.NumOperators;
internal class NumExponentiation : Operator
{
    private static NumExponentiation _instance = new();

    public static NumExponentiation Instance => _instance;

    private NumExponentiation() : base(NumExpFunc.Instance.Arguments, NumExpFunc.Instance.Returns, OperatorType.Exponentiation, NumExpFunc.Instance)
    {
    }

    private class NumExpFunc : IFunction
    {
        public Guid MemberId { get; init; } = Guid.NewGuid();

        private static NumExpFunc _instance = new();

        public static NumExpFunc Instance => _instance;

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

            var a = ((NumInstance)instances[0]).Value;
            var b = ((NumInstance)instances[1]).Value;

            if (a.GetType() == b.GetType() && a.GetType() == typeof(double))
            {
                return new NumInstance(Math.Pow(a, b));
            }
            else
            {
                try
                {
                    return new NumInstance(DecimalPow((decimal)a, (decimal)b));
                }
                catch { return new NumInstance(double.NaN); }
            }
        }

        private static decimal DecimalLn(decimal x)
        {
            // Decreasing the value of x to speed up convergence
            int n = 0;
            while (x > 2)
            {
                x /= (decimal)Math.E;
                n++;
            }

            decimal sum = 0m;
            decimal term = (x - 1) / (x + 1);
            decimal termSquared = term * term;
            decimal currentTerm = term;
            int k = 1;

            while (k < 1000) // Zoom in for more accuracy
            {
                sum += currentTerm / k;
                k += 2;
                currentTerm *= termSquared;
            }

            return 2 * sum + n;
        }
        private static decimal DecimalExp(decimal power)
        {
            decimal result = 1m;
            decimal term = 1m;
            for (int i = 1; i <= 100; i++) // Increase the number of iterations for greater accuracy
            {
                term *= power / i;
                result += term;
            }
            return result;
        }
        private static decimal DecimalPow(decimal baseNumber, decimal exponent)
        {
            return DecimalExp(exponent * DecimalLn(baseNumber));
        }
    }
}
