using SPL.System.Instances;
using SPL.System.Types;
using System.Linq;

namespace SPL.System.Statements;
public class WhileStatement : IStatement, IStatementList
{
    private readonly Dictionary<string, IInstance<IType>> _values;

    public LinkedList<IStatement> Statements { get; init; }

    public IScope Parent { get; init; }

    public Dictionary<string, IInstance<IType>> Values => _values.Concat(Parent.Values).ToDictionary(k => k.Key, v => v.Value);

    public WhileStatement(LinkedList<IStatement> statements, IScope parentScope)
    {
        Statements = statements;
        Parent = parentScope;
        _values = new();
    }
}
