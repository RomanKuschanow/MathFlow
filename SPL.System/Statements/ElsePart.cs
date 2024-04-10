using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class ElsePart : IStatementList
{
    public LinkedList<IStatement> Statements { get; init; }

    public IScope Parent { get; init; }

    public List<Variable> Variables { get; init; }

    public ElsePart(LinkedList<IStatement> statements, IScope parentScope)
    {
        Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Parent = parentScope ?? throw new ArgumentNullException(nameof(parentScope));
        Variables = new();
    }

    public List<Variable> GetAllVariablesInScope() => Variables.Concat(Parent.GetAllVariablesInScope()).ToList();
}
