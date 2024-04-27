#nullable disable
using Lexer;
using Lexer.LexemeDefinitions;
using SPL.SemanticAnalyzer;
using SPL.System;
using SPL.System.Types;
using SyntaxAnalyzer;
using SyntaxAnalyzer.Generator;
using SyntaxAnalyzer.Rules;
using SyntaxAnalyzer.Tokens;

namespace SPL;
public class SPLProgram
{
    private static List<ILexemeDefinition> _lexemesDefinition = new()
    {
        new RegexLexemeDefinition(new(@"^(?:if|else|while|break|continue|print|p|input)"), "keyword"),
        new RegexLexemeDefinition(new($@"^(:{string.Join("|", typeof(IType).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IType))).Select(t => t.GetProperty("Instance").GetValue(null) as IType).SelectMany(t => t.Aliases))})"), "type"),
        new RegexLexemeDefinition(new(@"^(?:\b\d+\b(?!f|\.))"), "int"),
        new RegexLexemeDefinition(new(@"^(?:\b\d+(?:\.\d+)?(?:[Ee][+-]?\d+)?f?)"), "float"),
        new RegexLexemeDefinition(new(@"^(?:true|false)"), "bool"),
        new RegexLexemeDefinition(new(@"^(?:[\p{L}_]+[\p{L}_\d]*)"), "id"),
        new StringLexemeDefinition(),
        new RegexLexemeDefinition(new(@"^(?:<=|>=|==|!=|\|\||&&|\*\*|[=!><*/+%-])"), "operator"),
        new RegexLexemeDefinition(new(@"^(?:[(){},;])"), "separator"),
        new RegexLexemeDefinition(new(@"^\s"), "whitespace", true),
    };
    private static List<List<string>> _rawGrammar = new()
    {
        new() { "Program", "StatementList" },
        new() { "StatementList", "StatementList", "Statement" },
        new() { "StatementList", "Statement" },
        new() { "Statement", "Declaration", ";" },
        new() { "Statement", "Assignment", ";" },
        new() { "Statement", "PrintStatement", ";" },
        new() { "Statement", "IfStatement" },
        new() { "Statement", "WhileStatement" },
        new() { "Statement", "BreakStatement", ";" },
        new() { "BreakStatement", "break" },
        new() { "BreakStatement", "continue" },
        new() { "Declaration", "type", "id" },
        new() { "Declaration", "type", "id", "=", "Expression" },
        new() { "Assignment", "id", "=", "Expression" },
        new() { "PrintStatement", "print", "(", "Args", ")" },
        new() { "PrintStatement", "p", "Expression" },
        new() { "IfStatement", "if", "(", "Expression", ")", "Block", "ElsePart" },
        new() { "ElsePart", "else", "Block" },
        new() { "ElsePart", "" },
        new() { "WhileStatement", "while", "(", "Expression", ")", "Block" },
        new() { "Block", "{", "StatementList", "}" },
        new() { "Block", "Statement" },
        new() { "Expression", "Expression", "||", "And" },
        new() { "Expression", "And" },
        new() { "And", "And", "&&", "Equality" },
        new() { "And", "Equality" },
        new() { "Equality", "Equality", "==", "Relational" },
        new() { "Equality", "Equality", "!=", "Relational" },
        new() { "Equality", "Relational" },
        new() { "Relational", "Relational", "<", "Additive" },
        new() { "Relational", "Relational", ">", "Additive" },
        new() { "Relational", "Relational", "<=", "Additive" },
        new() { "Relational", "Relational", ">=", "Additive" },
        new() { "Relational", "Additive" },
        new() { "Additive", "Additive", "+", "Multiplicative" },
        new() { "Additive", "Additive", "-", "Multiplicative" },
        new() { "Additive", "Multiplicative" },
        new() { "Multiplicative", "Multiplicative", "*", "Exponent" },
        new() { "Multiplicative", "Multiplicative", "/", "Exponent" },
        new() { "Multiplicative", "Multiplicative", "%", "Exponent" },
        new() { "Multiplicative", "Exponent" },
        new() { "Exponent", "Factor", "**", "Exponent" },
        new() { "Exponent", "Factor" },
        new() { "Factor", "(", "Expression", ")" },
        new() { "Factor", "Value" },
        new() { "Factor", "id" },
        new() { "Factor", "Negation" },
        new() { "Factor", "Not" },
        new() { "Factor", "Factorial" },
        new() { "Factor", "Input" },
        new() { "Factor", "Cast" },
        new() { "Value", "int" },
        new() { "Value", "float" },
        new() { "Value", "bool" },
        new() { "Value", "string" },
        new() { "Negation", "-", "Factor" },
        new() { "Not", "!", "Factor" },
        new() { "Factorial", "Factor", "!" },
        new() { "Input", "input", "(", ")" },
        new() { "Input", "input", "(", "Args", ")" },
        new() { "Cast", "(", "type", ")", "Factor" },
        new() { "Args", "Args", ",", "Expression" },
        new() { "Args", "Expression" }
    };

    private Program _program;
    private string _code;
    private List<Action<string>> _outs;
    private Func<string, Task<string>> _in;

    private static Grammar _grammar;
    private static Parser _parser;

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
        try
        {
            await _program.Execute(_outs, _in);
        }
        catch (Exception e)
        {
            foreach (Action<string> _out in _outs)
                _out(e.Message);
        }
    }

    public async Task Build()
    {
        await Task.Run(() =>
        {
            LexemeAnalyzer lexer = new(_lexemesDefinition);

            Analyzer analyzer = new();

                var lexemes = lexer.Analyze(_code);
                var tree = _parser.Parse(GetParserStack(lexemes, _grammar));
                _program = analyzer.Analyze(tree);
            try
            {
            }
            catch (Exception e)
            {
                foreach (Action<string> _out in _outs)
                    _out(e.Message);
            }
        });
    }

    private Stack<IToken> GetParserStack(IEnumerable<Lexeme> lexemes, Grammar grammar)
    {
        Stack<IToken> result = new();
        result.Push(new InputEnd());

        foreach (Lexeme lexeme in lexemes.Reverse())
        {
            string symbolValue = lexeme.Type;

            if (!new string[] { "type", "id", "int", "float", "bool", "string" }.Contains(lexeme.Type))
            {
                symbolValue = lexeme.Value;
            }

            result.Push(new Terminal(grammar.Terminals.Single(t => t.Value == symbolValue), lexeme.Value));
        }

        return result;
    }

    public static void UpdateParser()
    {
        _grammar = new(_rawGrammar);
        RuleAnalyzer generator = new();
        _parser = new(generator.Analyze(_grammar));
    }
}
