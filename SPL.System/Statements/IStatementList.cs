using System.Collections.Immutable;

namespace SPL.System.Statements;
public interface IStatementList : IScope
{
    ImmutableList<IStatement> Statements { get; }
}
