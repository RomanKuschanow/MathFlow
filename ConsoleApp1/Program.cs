using MathFlow;

namespace ConsoleApp1;

internal class Program
{
    static void Main(string[] args)
    {
        Interpreter interpreter = new();

        MathFlow.ConsoleObservables.Console console = new();

        var program = interpreter.Analyze(
            """
            num x;
            x = 5 + 4;
            num y = x + 5;
            print(x);
            print(y);
            print(x + y);
            print(5 * 4);
            print(5+4*2);
            print((5+4)*2);
            """, console);

        program.Execute(console);
    }
}
