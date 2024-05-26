using SPL.System.Instances;
using System.Collections.Immutable;

namespace SPL.System.Statements;
public class Root : IStatementList
{
    private LinkedList<IStatement> _statements;

    public IScope Parent => null!;

    public List<Variable> Variables { get; init; }

    public ImmutableList<IStatement> Statements => ImmutableList.CreateRange(_statements);

    public Root(LinkedList<IStatement> statements)
    {
        _statements = statements ?? throw new ArgumentNullException(nameof(statements));
        Variables = new();
    }

    public void ClearVariables()
    {
        Variables.Clear();

        Statements.ForEach(s =>
        {
            if (s is IScope)
                ((IScope)s).ClearVariables();
        });
    }

    public List<Variable> GetAllVariablesInScope() => Variables;
}
