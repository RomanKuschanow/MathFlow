#nullable disable
using SPL;
using SPL.System.Statements;

namespace SPL.System;
public class Program
{
    private Root _root;

    private Action<string> _out;
    private Func<string, Task<string>> _in;

    private Stack<IStatement> _statements;

    public Program(Root root)
    {
        _root = root ?? throw new ArgumentNullException(nameof(root));
    }

    public async Task Execute(List<Action<string>> outs, Func<string, Task<string>> @in)
    {
        if (outs is null)
        {
            throw new ArgumentNullException(nameof(outs));
        }

        if (@in is null)
        {
            throw new ArgumentNullException(nameof(@in));
        }

        _out = null!;

        outs.ForEach(o => _out += o);
        _in = @in.GetInvocationList().Length == 1 ? @in : throw new InvalidDataException("input delegate must not be a type of 'MulticastDelegate'");

        _statements = new();

        PushToStack(_root.Statements);

        while (_statements.Count > 0)
            _statements.Pop().Execute();
    }

    public void ConsoleOut(string str) => _out(str);

    public async Task<string> ConsoleIn(string str = "") => await _in(str);

    public void PushToStack(IEnumerable<IStatement> statements)
    {
        statements = statements.Reverse();

        foreach (var statement in statements)
        {
            _statements.Push(statement);
        }
    }
}
