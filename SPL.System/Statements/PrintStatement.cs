#nullable disable
using SPL.System.Statements.Expressions;

namespace SPL.System.Statements;
public class PrintStatement : IStatement
{
    private readonly Action<string> _print;
    private readonly List<IExpression> _values;

    public PrintStatement(Action<string> print, List<IExpression> values)
    {
        _print = print ?? throw new ArgumentNullException(nameof(print));
        _values = values ?? throw new ArgumentNullException(nameof(values));
    }

    public void Execute() => _print(string.Join(" ", _values.Select(v => v.GetValue().ToString())));
}
