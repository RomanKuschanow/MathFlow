#nullable disable
using SPL.System.Statements.Expressions;

namespace SPL.System.Statements;
public class PrintStatement : IStatement
{
    private readonly Func<string, CancellationToken, Task> _print;
    private readonly List<IExpression> _values;

    public PrintStatement(Func<string, CancellationToken, Task> print, List<IExpression> values)
    {
        _print = print ?? throw new ArgumentNullException(nameof(print));
        _values = values ?? throw new ArgumentNullException(nameof(values));
    }

    public async Task Execute(CancellationToken ct) => await _print(string.Join(" ", _values.Select(v => v.GetValue().ToString())), ct);
}
