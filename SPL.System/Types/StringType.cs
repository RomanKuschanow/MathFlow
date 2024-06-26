﻿#nullable disable
using SPL.System.Instances;
using SPL.System.Operators;
using System.Collections.Immutable;

namespace SPL.System.Types;
public class StringType : IType
{
    private static StringType _instance = new();

    public static StringType Instance => _instance;

    private StringType() { }

    public string Name => "String";

    public ImmutableList<string> Aliases => ImmutableList.CreateRange(new[] { "String", "string", "str" });

    public IEnumerable<IOperator> Operators => new List<IOperator>()
    {
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance },
            resultType: Instance,
            operatorType: OperatorType.Addition,
            func: args =>
            {
                var result = Instance.GetInstance(((StringInstance)args[0]).Value + ((StringInstance)args[1]).Value);

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }), 
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance },
            resultType: Instance,
            operatorType: OperatorType.Equal,
            func: args =>
            {
                var result = BoolType.Instance.GetInstance(((StringInstance)args[0]).Value == ((StringInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }), 
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance },
            resultType: Instance,
            operatorType: OperatorType.NotEqual,
            func: args =>
            {
                var result = BoolType.Instance.GetInstance(((StringInstance)args[0]).Value != ((StringInstance)args[1]).Value);

                if (BoolType.Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }), 
        new Operator(
            operandTypes: new List<IType>() { Instance, FloatType.Instance },
            resultType: Instance,
            operatorType: OperatorType.Multiplication,
            func: args =>
            {
                var a = ((StringInstance)args[0]).Value;
                var b = ((FloatInstance)args[1]).Value;

                if (b != (int)b || b < 0)
                    throw new InvalidDataException();

                var result = Instance.GetInstance(string.Join("", Enumerable.Repeat(a, (int)b)));

                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }), 
    };

    public IInstance<IType> GetInstance(params object[] args)
    {
        if (args.Length == 0)
            return new StringInstance("");

        return new StringInstance((string)args[0]);
    }

    public bool IsInstance(IInstance<IType> instance) => instance is IInstance<StringType>;

    public IInstance<IType> Cast(IInstance<IType> instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        return new StringInstance(instance.ToString());
    }
}
