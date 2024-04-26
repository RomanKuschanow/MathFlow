using SPL.System.Instances;
using SPL.System.Operators;
using System.Collections.Immutable;

namespace SPL.System.Types;
public class BoolType : IType
{
    private static BoolType _instance = new();

    public static BoolType Instance => _instance;

    private BoolType() { }

    public string Name => "Bool";

    public ImmutableList<string> Aliases => ImmutableList.CreateRange(new[] { "Bool", "bool", "Boolean", "boolean" });

    public IEnumerable<IOperator> Operators => new List<IOperator>()
    {
        new Operator(
            operandTypes: new List<IType>() { Instance }, 
            resultType: Instance, 
            operatorType: OperatorType.Not,
            func: a => 
            {
                var result = Instance.GetInstance(!((BoolInstance)a[0]).Value); 
                
                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance }, 
            resultType: Instance, 
            operatorType: OperatorType.Or,
            func: a => 
            {
                var result = Instance.GetInstance(((BoolInstance)a[0]).Value || ((BoolInstance)a[1]).Value); 
                
                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance }, 
            resultType: Instance, 
            operatorType: OperatorType.And,
            func: a => 
            {
                var result = Instance.GetInstance(((BoolInstance)a[0]).Value && ((BoolInstance)a[1]).Value); 
                
                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance }, 
            resultType: Instance, 
            operatorType: OperatorType.Equal,
            func: a => 
            {
                var result = Instance.GetInstance(((BoolInstance)a[0]).Value == ((BoolInstance)a[1]).Value); 
                
                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
        new Operator(
            operandTypes: new List<IType>() { Instance, Instance }, 
            resultType: Instance, 
            operatorType: OperatorType.NotEqual,
            func: a => 
            {
                var result = Instance.GetInstance(((BoolInstance)a[0]).Value != ((BoolInstance)a[1]).Value); 
                
                if (Instance.IsInstance(result))
                    return result;
                else
                    throw new InvalidDataException();
            }),
    };


    public IInstance<IType> GetInstance(params object[] args)
    {
        if (args.Length == 0)
            return new BoolInstance();

        return new BoolInstance((bool)args[0]);
    }

    public bool IsInstance(IInstance<IType> instance) => instance is IInstance<BoolType>;

    public IInstance<IType> Cast(IInstance<IType> instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance.Type is BoolType)
            return instance;

        if (instance.Type is FloatType)
            return new BoolInstance(((FloatInstance)instance).Value == 0);

        if (instance.Type is IntType)
            return new BoolInstance(((IntInstance)instance).Value == 0);

        if (instance.Type is StringType)
            return new BoolInstance(bool.Parse(((StringInstance)instance).Value));

        throw new InvalidOperationException();
    }
}
