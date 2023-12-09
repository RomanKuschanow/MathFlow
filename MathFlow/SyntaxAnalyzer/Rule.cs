using System.Collections.Immutable;

namespace MathFlow.SyntaxAnalyzer;
public class Rule : IRule
{
    public string NonTerminal { get; init; }
    public ImmutableList<string> Tokens { get; init; }

    public Rule(string nonTerminal, params string[] tokens)
    {
        NonTerminal = nonTerminal;
        Tokens = ImmutableList.CreateRange(tokens);
    }
}
