using SyntaxAnalyzer.Rules.Symbols;
using System.Collections.Immutable;

namespace SyntaxAnalyzer.Rules;
public record Rule : IRule
{
    public NonterminalSymbol NonTerminal { get; init; }
    public ImmutableArray<ISymbol> Tokens { get; init; }

    public Rule(NonterminalSymbol nonTerminal, params ISymbol[] tokens)
    {
        NonTerminal = nonTerminal ?? throw new ArgumentNullException(nameof(nonTerminal));
        Tokens = tokens.ToImmutableArray();
    }

    public override string ToString() => $"{NonTerminal} -> {string.Join(" ", Tokens)}";
}
