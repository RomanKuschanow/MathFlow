using SPL.System.Instances;
using SPL.System.Types;

namespace SPL.System.Statements;
public class Root : IStatementList
{
    public IScope Parent => null!;

    public List<Variable> Variables { get; init; }

    public LinkedList<IStatement> Statements { get; init; }

    public Root(LinkedList<IStatement> statements)
    {
        Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Variables = new();
    }

    public List<Variable> GetAllVariablesInScope() => Variables;
}
