using SPL.System.Instances;
using SPL.System.Statements.Expressions;
using SPL.System.Types;

namespace SPL.System.Statements;
public class Assignment : IStatement
{
    private readonly Action<string, IInstance<IType>> _assignValue;
    private readonly string _name;
    private readonly IExpression _value;

    public Assignment(Action<string, IInstance<IType>> assignValue, string name, IExpression value = null!)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        _assignValue = assignValue ?? throw new ArgumentNullException(nameof(assignValue));
        _name = name;
        _value = value;
    }

    public async Task Execute(CancellationToken ct) => await Task.Run(() => _assignValue(_name, _value.GetValue()), ct);
}
