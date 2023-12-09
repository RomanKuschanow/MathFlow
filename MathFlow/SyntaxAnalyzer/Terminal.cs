using MathFlow.LexemeAnalyzer;

namespace MathFlow.SyntaxAnalyzer;
public class Terminal : IToken
{
    public string Name { get; init; }

    public Lexeme Value { get; init; }

    public Terminal(string name, Lexeme value)
    {
        Name = name;
        Value = value;
    }
}
