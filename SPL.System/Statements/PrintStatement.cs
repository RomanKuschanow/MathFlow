#nullable disable
using SPL.System.Statements.Expressions;

namespace SPL.System.Statements;
public class PrintStatement : IStatement
{
    private readonly Action<string> _print;
    private readonly IExpression _value;

    public PrintStatement(Action<string> print, IExpression value)
    {
        _print = print ?? throw new ArgumentNullException(nameof(print));
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void Execute() => _print(_value.GetValue().ToString());
}
