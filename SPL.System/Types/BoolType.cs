using SPL.System.Instances;
using SPL.System.Operators;

namespace SPL.System.Types;
public class BoolType : IType
{
    private static BoolType _instance = new();

    public static BoolType Instance => _instance;

    private BoolType() { }

    public string Name => "Bool";

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

    public IInstance<IType> GetInstance(params object[] args) => (IInstance<IType>)new BoolInstance((bool)args[0]);

    public bool IsInstance(IInstance<IType> instance) => instance is IInstance<BoolType>;
}
