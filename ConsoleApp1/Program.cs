using MathFlow;
using System.Diagnostics;

namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {
        Interpreter interpreter = new();

        MathFlow.ConsoleObservables.Console console = new();

        Stopwatch stopwatch = new();

        var program = interpreter.Analyze(
            """
            num x;
            x = 5 + 4;
            num y = x + 5;
            print(-x);
            print(y);
            print(x + y);
            print(5 * 4);
            print(5+4*2);
            print(-(5+4)*2);
            print(5/7);
            print(5/7m);
            print(1.62345e2m);
            """, console);

        stopwatch.Start();
        program.Execute(console);
        stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Execution time in milliseconds = {stopwatch.ElapsedMilliseconds}");
    }
}
