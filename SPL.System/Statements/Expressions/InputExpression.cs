#nullable disable
using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class InputExpression : IExpression
{
    private readonly Func<string, CancellationToken, Task<string>> _input;
    private readonly List<IExpression> _values;

    public InputExpression(Func<string, CancellationToken, Task<string>> input, List<IExpression> values = null!)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _values = values ?? new List<IExpression>() { new InstanceExpression(StringType.Instance.GetInstance()) };
    }

    public async Task<IInstance<IType>> GetValue(CancellationToken ct)
    {
        string result = await _input(string.Join(" ", _values.Select(v => v.GetValue(ct).GetAwaiter().GetResult().ToString())), ct);

        return new StringInstance(result);
    }
}
