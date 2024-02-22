using System.Collections.Immutable;

namespace MathFlow.SemanticAnalyzer.Statements;
public interface IStatementList
{
    public ImmutableList<IStatement> Statements { get; }
}
