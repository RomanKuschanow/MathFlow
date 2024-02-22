using MathFlow.ConsoleObservables;
using MathFlow.SemanticAnalyzer;
using SyntaxAnalyzer;

namespace MathFlow;
public class Interpreter
{
    //public Program Analyze(string text, params IConsoleObserver[] observers)
    //{
    //    Lexer lexer = new(Constants.LexemeDefinitions);
    //    LexemesToTokensConverter converter = new();
    //    Parser parser = new(Constants.Rules, Constants.Actions, Constants.Goto);
    //    Analyzer analyzer = new();

    //    try
    //    {
    //        var lexemes = lexer.Analyze(text);
    //        var syntaxTree = parser.Parse(converter.Convert(lexemes));
    //        var program = analyzer.Analyze(syntaxTree);

    //        foreach (var observer in observers)
    //        {
    //            observer.OnNext("Build Succeed");
    //        }

    //        return program;
    //    }
    //    catch (Exception ex)
    //    {
    //        foreach (var observer in observers)
    //        {
    //            observer.OnNext(ex.Message);
    //        }
    //        return new SemanticTree(new());
    //    }
    //}
}
