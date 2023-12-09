using System.Collections.Immutable;

namespace MathFlow.SyntaxAnalyzer;
public class NonTerminal : IToken
{
    public string Name { get; init; }

    public ImmutableList<IToken> Tokens { get; init; }

    public NonTerminal(string name, IEnumerable<IToken> tokens)
    {
        Name = name;
        Tokens = ImmutableList.CreateRange(tokens);
    }
}
