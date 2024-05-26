using System.Collections.Immutable;

namespace SPL.System.Statements;
public interface IStatementList : IScope
{
    ImmutableList<IStatement> Statements { get; }

    void IScope.ClearVariables()
    {
        Variables.Clear();

        Statements.ForEach(s =>
        {
            if (s is IScope)
                ((IScope)s).ClearVariables();
        });
    }
}
