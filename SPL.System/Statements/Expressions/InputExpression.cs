#nullable disable
using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class InputExpression : IExpression
{
    private readonly Func<string, Task<string>> _input;
    private readonly List<IExpression> _values;

    public InputExpression(Func<string, Task<string>> input, List<IExpression> values = null!)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _values = values ?? new List<IExpression>() { new InstanceExpression(StringType.Instance.GetInstance()) };
    }


    public IInstance<IType> GetValue()
    {
        string result = _input(string.Join(" ", _values.Select(v => v.GetValue().ToString()))).GetAwaiter().GetResult();

        return (IInstance<IType>)new StringInstance(result);
    }
}
