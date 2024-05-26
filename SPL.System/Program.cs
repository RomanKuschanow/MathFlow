#nullable disable
using SPL;
using SPL.System.Operators;
using SPL.System.Statements;
using SPL.System.Types;

namespace SPL.System;
public class Program
{
    private Root _root;

    private Func<string, CancellationToken, Task> _out;
    private Func<string, CancellationToken, Task<string>> _in;

    private Stack<IStatement> _statements;

    public TypeManager TypeManager { get; init; }
    public OperatorsManager OperatorsManager { get; init; }

    public Program(Root root, TypeManager typeManager, OperatorsManager operatorsManager)
    {
        _root = root ?? throw new ArgumentNullException(nameof(root));
        TypeManager = typeManager ?? throw new ArgumentNullException(nameof(typeManager));
        OperatorsManager = operatorsManager ?? throw new ArgumentNullException(nameof(operatorsManager));
    }

    public async Task Execute(List<Func<string, CancellationToken, Task>> outs, Func<string, CancellationToken, Task<string>> @in, CancellationToken ct)
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
        _root.ClearVariables();

        PushToStack(_root.Statements);

        await Task.Run(async () =>
        {
            while (_statements.Count > 0 && !ct.IsCancellationRequested)
                await _statements.Pop().Execute(ct).WaitAsync(ct);
        }, ct);
    }

    public async Task ConsoleOut(string str, CancellationToken ct)
    {
        await _out(str, ct);
        await Task.Delay(1, ct);
    }

    public async Task<string> ConsoleIn(string str, CancellationToken ct) => await _in(str is null ? "" : str, ct);

    public void PushToStack(IEnumerable<IStatement> statements)
    {
        statements = statements.Reverse();

        foreach (var statement in statements)
        {
            _statements.Push(statement);
        }
    }

    public void BreakLoop(bool _continue)
    {
        if (_statements.SingleOrDefault(s => s is WhileStatement) is null)
        {
            throw new InvalidOperationException();
        }    

        while (_statements.Peek() is not WhileStatement)
        {
            _statements.Pop();
        }

        if (!_continue)
        {
            _statements.Pop();
        }
    }
}
