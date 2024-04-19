using SPL.System.Instances;
using SPL.System.Types;
using System.Collections.Immutable;

namespace SPL.System.Statements;
public class ElsePart : IStatementList
{
    private LinkedList<IStatement> _statements;

    public ImmutableList<IStatement> Statements => ImmutableList.CreateRange(_statements);

    public IScope Parent { get; init; }

    public List<Variable> Variables { get; init; }

    public ElsePart(LinkedList<IStatement> statements, IScope parentScope)
    {
        _statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Parent = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
        Variables = new();
    }

    public List<Variable> GetAllVariablesInScope() => Variables.Concat(Parent.GetAllVariablesInScope()).ToList();
}
