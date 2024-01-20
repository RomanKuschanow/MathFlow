using MathFlow.TypeSystem.Instances;

namespace MathFlow.TypeSystem.Functions;
public class PrintFunction : IFunction
{
    public List<Variable> Arguments => new()
    {
        new("string", Types.String.Instance)
    };

    public Type Returns => Types.Void.Instance;

    private Action<string> _print;

    public PrintFunction(Action<string> print)
    {
        _print = print;
    }

    public IInstance Execute(params IInstance[] instances)
    {
        if (instances.Length != 1)
        {
            throw new ArgumentException($"No overload for method 'print' takes {instances.Length} arguments");
        }

        _print((instances[0] ?? throw new ArgumentNullException("string")).ToString() ?? "");
        return new VoidInstance();
    }
}
