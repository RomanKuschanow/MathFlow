namespace SPL.System.Statements;
public interface IStatementList : IScope
{
    LinkedList<IStatement> Statements { get; }
}
