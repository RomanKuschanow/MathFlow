namespace MathFlow.SemanticAnalyzer.Statements;
public class PrintStatement : IStatement
{
    private readonly Func<string> _getString;

    private readonly Action<string> _print;

    public PrintStatement(Action<string> print, Func<string> getString)
    {
        _print = print ?? throw new ArgumentNullException(nameof(print));
        _getString = getString ?? throw new ArgumentNullException(nameof(getString));
    }

    public void Execute() => _print(_getString());
}
