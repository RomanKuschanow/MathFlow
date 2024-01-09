
namespace MathFlow.ConsoleObservables;
public class Console : IConsoleObserver
{
    public void OnCompleted() => System.Console.WriteLine("Execute finished");

    public void OnError(Exception exception) => System.Console.WriteLine(exception.Message);

    public void OnNext(string str) => System.Console.WriteLine(str);
}
