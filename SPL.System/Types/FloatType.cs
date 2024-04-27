using SPL.System.Instances;
using SPL.System.Operators;
using System.Collections.Immutable;

namespace SPL.System.Types;
public class FloatType : IType
{
    private static FloatType _instance = new();

    public static FloatType Instance => _instance;

    private FloatType() { }

    public string Name => "Float";

    public ImmutableList<string> Aliases => ImmutableList.CreateRange(new[] { "Float", "float" });

    public IEnumerable<IOperator> Operators => new List<IOperator>()
    {
        new Operator(
            operandTypes: new List<IType>() { Instance },
            resultType: Instance,
            operatorType: OperatorType.Negation,
            func: args =>
            {
                var result = Instance.GetInstance(-((FloatInstance)args[0]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Equal,
            func: args =>
            {
                args = ConvertToFloat(args);
                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value == ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.NotEqual,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value != ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.LessThan,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value < ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.LessThanOrEqual,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value <= ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.GreaterThan,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value > ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.GreaterThanOrEqual,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = BoolType.Instance.GetInstance(((FloatInstance)args[0]).Value >= ((FloatInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Addition,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = Instance.GetInstance(((FloatInstance)args[0]).Value + ((FloatInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Subtraction,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = Instance.GetInstance(((FloatInstance)args[0]).Value - ((FloatInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Multiplication,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = Instance.GetInstance(((FloatInstance)args[0]).Value * ((FloatInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Division,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = Instance.GetInstance(((FloatInstance)args[0]).Value / ((FloatInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Mod,
            func: args =>
            {
                args = ConvertToFloat(args);

                var result = Instance.GetInstance(((FloatInstance)args[0]).Value % ((FloatInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<List<IType>>() { new() { Instance, Instance }, new() { IntType.Instance, Instance }, new() { Instance, IntType.Instance }, },
            resultType: Instance,
            operatorType: OperatorType.Exponent,
            func: args =>
            {
                args = ConvertToFloat(args);

                var a = ((FloatInstance)args[1]).Value;
                var b = ((FloatInstance)args[0]).Value;

                var result = Instance.GetInstance(Math.Pow(a, b));

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<IType>() { Instance },
            resultType: Instance,
            operatorType: OperatorType.Factorial,
            func: args =>
            {
                var a = ((FloatInstance)args[0]).Value;

                var result = Instance.GetInstance(Gamma(a + 1));

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();

                double GammaApprox(double x)
                {
                    double[] p = { -1.71618513886549492533811e+0, 2.47656508055759199108314e+1, -3.79804256470945635097577e+2, 6.29331155312818442661052e+2, 8.66966202790413211295064e+2, -3.14512729688483675254357e+4, -3.61444134186911729807069e+4, 6.64561438202405440627855e+4 };

                    double[] q = { -3.08402300119738975254353e+1, 3.15350626979604161529144e+2, -1.01515636749021914166146e+3, -3.10777167157231109440444e+3, 2.25381184209801510330112e+4, 4.75584627752788110767815e+3, -1.34659959864969306392456e+5, -1.15132259675553483497211e+5 };
                    double z = x - 1.0;
                    double a = 0.0;
                    double b = 1.0;
                    int i;
                    for (i = 0; i < 8; i++)
                    {
                        a = (a + p[i]) * z;
                        b = b * z + q[i];
                    }
                    return (a / b + 1.0);
                }

                double Gamma(double z)
                {

                    if ((z > 0) && (z < 1.0))
                    {
                        return Gamma(z + 1.0) / z;
                    }

                    if (z > 2)
                    {
                        return (z - 1) * Gamma(z - 1);
                    }

                    if (z <= 0)
                    {
                        return Math.PI / (Math.Sin(Math.PI * z) * Gamma(1 - z));
                    }

                    return GammaApprox(z);
                }
            }),
    };

    public IInstance<IType> GetInstance(params object[] args)
    {
        if (args.Length == 0)
            return new FloatInstance(0);

        return new FloatInstance((double)args[0]);
    }

    public bool IsInstance(IInstance<IType> instance) => instance is IInstance<FloatType>;

    private List<IInstance<IType>> ConvertToFloat(List<IInstance<IType>> args)
    {
        for (int i = 0; i < args.Count; i++)
        {
            if (args[i] is IntInstance)
                args[i] = (FloatInstance)(args[i] as IntInstance)!;
        }

        return args;
    }

    public IInstance<IType> Cast(IInstance<IType> instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance.Type is FloatType)
            return instance;

        if (instance.Type is BoolType)
            return new FloatInstance(((BoolInstance)instance).Value ? 1 : 0);

        if (instance.Type is IntType)
            return new FloatInstance(Convert.ToDouble(((IntInstance)instance).Value));

        if (instance.Type is StringType)
            return new FloatInstance(double.Parse(((StringInstance)instance).Value));

        throw new InvalidOperationException();
    }
}
