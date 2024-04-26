using Lexer;
using Lexer.LexemeDefinitions;
using SPL.SemanticAnalyzer;
using SPL.System;
using SyntaxAnalyzer;
using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Tokens;

namespace SPL;
public class SPLProgram
{
    private static List<ILexemeDefinition> _lexemesDefinition = new()
    {

    };
    private static List<List<string>> _grammar = new()
    {
        new() { "" },
    };

    private Program _program;
    private string _code;
    private List<Action<string>> _outs;
    private Func<string, Task<string>> _in;

    public SPLProgram(string code, List<Action<string>> outs, Func<string, Task<string>> @in)
    {
        _code = code ?? throw new ArgumentNullException(nameof(code));
        SetInOut(outs, @in);
    }

    public void SetInOut(List<Action<string>> outs, Func<string, Task<string>> @in)
    {
        _outs = outs;
        _in = @in;
    }

    public async Task Execute()
    {
        await _program.Execute(_outs, _in);
    }

    private void Build()
    {
        LexemeAnalyzer lexer = new(_lexemesDefinition);

        Grammar grammar = new(_grammar);
        RuleAnalyzer generator = new();
        Parser parser = new(generator.Analyze(grammar));

        Analyzer analyzer = new();

        try
        {
            var lexemes = lexer.Analyze(_code);
            var tree = parser.Parse(GetParserStack(lexemes, grammar));
            _program = analyzer.Analyze(tree);
        }
        catch (Exception e)
        {
            foreach (Action<string> _out in _outs)
                _out(e.Message);
        }
    }

    private Stack<IToken> GetParserStack(IEnumerable<Lexeme> lexemes, Grammar grammar)
    {
        Stack<IToken> result = new();
        result.Push(new InputEnd());

        foreach (Lexeme lexeme in lexemes.Reverse())
            result.Push(new Terminal(grammar.Terminals.Single(t => t.Value == lexeme.Type), lexeme.Value));

        return result;
    }
}
