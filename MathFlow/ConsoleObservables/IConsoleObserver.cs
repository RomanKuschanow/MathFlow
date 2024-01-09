namespace MathFlow.ConsoleObservables;
public interface IConsoleObserver
{
    public void OnNext(string str);
    public void OnError(Exception exception);
    public void OnCompleted();
}
