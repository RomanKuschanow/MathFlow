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
        string code = "";

        try
        {
            if (args[0] == "-f")
            {
                code = File.ReadAllText(args[1]);
            }
            else
            {
                code = "p" + string.Join(" ", args) + ";";
            }
        }
        catch
        {
            Console.WriteLine("Invalid arguments");
            return;
        }

        SPLProgram program = new(code, new() {async (str, ct) => await Task.Run(() => Console.WriteLine(str), ct) }, Input);

        SPLProgram.UpdateParser();

        CancellationTokenSource source = new();
        var ct = source.Token;

        await program.Build(ct);
        await program.Execute(ct);
    }

    private static async Task<string> Input(string str, CancellationToken ct)
    {
        Console.Write(str);
        return await Task.Run(Console.ReadLine, ct);
    }
}
