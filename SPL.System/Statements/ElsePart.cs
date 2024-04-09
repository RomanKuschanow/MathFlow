using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class ElsePart : IStatementList
{
    private readonly Dictionary<string, IInstance<IType>> _values;

    public LinkedList<IStatement> Statements { get; init; }

    public IScope Parent { get; init; }

    public Dictionary<string, IInstance<IType>> Values => _values.Concat(Parent.Values).ToDictionary(k => k.Key, v => v.Value);

    public ElsePart(LinkedList<IStatement> statements, IScope parentScope)
    {
        Statements = statements;
        Parent = parentScope;
        _values = new();
    }
}
