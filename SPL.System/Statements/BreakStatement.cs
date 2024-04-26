namespace SPL.System.Statements;
public class BreakStatement : IStatement
{
    private readonly Action<bool> _breakLoop;
    private readonly bool _continue;

    public BreakStatement(Action<bool> breakLoop, bool @continue)
    {
        _breakLoop = breakLoop ?? throw new ArgumentNullException(nameof(breakLoop));
        _continue = @continue;
    }

    public void Execute() => _breakLoop(_continue);
}
