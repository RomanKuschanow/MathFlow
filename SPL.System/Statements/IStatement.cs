namespace SPL.System.Statements;
public interface IStatement
{
    Task Execute(CancellationToken ct);
}
