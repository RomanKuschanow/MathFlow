using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements.Expressions;
public class VariableExpression : IExpression
{
    private readonly string _name;
    private readonly Func<string, IInstance<IType>> _getValue;

    public VariableExpression(string name, Func<string, IInstance<IType>> getValue)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _name = name;
        _getValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
    }

    public async Task<IInstance<IType>> GetValue(CancellationToken ct) => await Task.FromResult(_getValue(_name));
}
