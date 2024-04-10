#nullable disable
using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class InputExpression : IExpression
{
    private readonly Func<string, IInstance<StringType>> _input;
    private readonly IExpression _value;

    public InputExpression(Func<string, IInstance<StringType>> input, IExpression value = null!)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _value = value ?? new InstanceExpression(StringType.Instance.GetInstance());
    }


    public IInstance<IType> GetValue() => (IInstance<IType>)_input(_value.GetValue().ToString());
}
