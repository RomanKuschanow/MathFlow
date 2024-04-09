using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class IfStatement : IStatement, IStatementList
{
    private readonly ElsePart _else;
    private readonly Dictionary<string, IInstance<IType>> _values;

    public LinkedList<IStatement> Statements { get; init; }

    public IScope Parent { get; init; }

    public Dictionary<string, IInstance<IType>> Values => _values.Concat(Parent.Values).ToDictionary(k => k.Key, v => v.Value);

    public IfStatement(LinkedList<IStatement> statements, IScope parentScope, ElsePart elsePart = null!)
    {
        Statements = statements;
        Parent = parentScope;
        _values = new();
        _else = elsePart;
    }
}
