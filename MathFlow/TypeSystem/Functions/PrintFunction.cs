using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Instances;
using System.Collections.Immutable;

namespace MathFlow.TypeSystem.Functions;
public class PrintFunction : IFunction
{
    public Guid MemberId { get; init; } = Guid.NewGuid();

    public ImmutableList<Variable> Arguments => new List<Variable>()
    {
        new("string", Types.String.Instance)
    }.ToImmutableList();

    public Type Returns => Types.Void.Instance;

    private Action<string> _print;

    public PrintFunction(Action<string> print)
    {
        _print = print;
    }

    public IInstance Execute(IScope scope = null!, params IInstance[] instances)
    {
        if (instances.Length != 1)
        {
            throw new ArgumentException($"No overload for method 'print' takes {instances.Length} arguments");
        }

        _print((instances[0] ?? throw new ArgumentNullException("string")).ToString() ?? "");
        return new VoidInstance();
    }
}
