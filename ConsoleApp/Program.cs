#nullable disable
using SPL;
using SPL.System.Types;
using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        var code =
            """
            int i = 0;
            while (i < 10)
            {
                i = i + 1;
                if (i > 5)
                {
                    continue;
                }
                print(i);
            }
            print("a " + (string)i);
            """;

        Stopwatch stopwatch = new Stopwatch();

        SPLProgram program = new(code, new() { Console.WriteLine }, Input);

        SPLProgram.UpdateParser();

        stopwatch.Start();
        await program.Build();
        stopwatch.Stop();
        Console.WriteLine("Build time " + stopwatch.ElapsedMilliseconds);
        stopwatch.Restart();
        await program.Execute();
        stopwatch.Stop();
        Console.WriteLine("Run time " + stopwatch.ElapsedMilliseconds);
    }

    private static async Task<string> Input(string str)
    {
        Console.Write(str);
        return await Task.Run(Console.ReadLine);
    }
}
