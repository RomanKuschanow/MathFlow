using MathFlow.ConsoleObservables;
using MathFlow.SemanticAnalyzer.Datatypes;
using MathFlow.SemanticAnalyzer.Expression;
using MathFlow.SemanticAnalyzer.Statements;
using System.Collections.Immutable;
using System.Reactive.Subjects;

namespace MathFlow.SemanticAnalyzer;
public class SemanticTree
{
    private IEnumerable<IStatement> _statements;

    public SemanticTree(List<IStatement> statements)
    {
        _statements = statements ?? throw new ArgumentNullException(nameof(statements));
    }

    private Dictionary<string, object> heap = new();

    private Subject<string> console = new();

    internal void Complete() => _statements = _statements.ToImmutableList();

    public void Execute(params IConsoleObserver[] observers)
    {
        console = new();
        heap = new();

        foreach (var observer in observers)
        {
            console.Subscribe(
                onNext: observer.OnNext,
                onError: observer.OnError,
                onCompleted: observer.OnCompleted);
        }

        foreach (var statement in _statements)
        {
            try
            {
                statement.Execute();
            }
            catch (Exception e)
            {
                console.OnError(e);
                break;
            }
        }

        console.OnCompleted();

        console.Dispose();
    }

    internal void Declare(string type, string name, IExpression value) => heap.Add(name, value.GetValue());

    internal void Assign(string name, IExpression value) => heap[name] = value.GetValue();

    internal Num GetValue(string name) => (Num)heap[name];

    internal void Print(string str) => console.OnNext(str);
}
